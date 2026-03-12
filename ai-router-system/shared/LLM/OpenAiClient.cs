using System.Net.Http.Json;
using System.Text.Json;
using AiRouter.Shared.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace AiRouter.Shared.LLM;

public sealed class OpenAiClient(HttpClient httpClient, IOptions<OpenAiOptions> options, ILogger<OpenAiClient> logger) : IOpenAiClient
{
    private readonly OpenAiOptions _options = options.Value;

    public async Task<string> ClassifyIntent(string query, CancellationToken cancellationToken = default)
    {
        const string classifierPrompt = "Classify the user intent into one label only: flight_search, hotel_search, itinerary_planning, restaurant_recommendation. Return only the label.";
        var result = await SendChatCompletion(classifierPrompt, query, cancellationToken);
        var normalized = result.Trim().ToLowerInvariant();

        if (!IntentCategory.Allowed.Contains(normalized))
        {
            logger.LogWarning("Unknown classification '{Intent}'. Falling back to itinerary_planning.", normalized);
            return IntentCategory.ItineraryPlanning;
        }

        return normalized;
    }

    public Task<string> RunAgent(string systemPrompt, string userPrompt, CancellationToken cancellationToken = default) =>
        SendChatCompletion(systemPrompt, userPrompt, cancellationToken);

    private async Task<string> SendChatCompletion(string systemPrompt, string userPrompt, CancellationToken cancellationToken)
    {
        var request = new
        {
            model = _options.Model,
            messages = new object[]
            {
                new { role = "system", content = systemPrompt },
                new { role = "user", content = userPrompt }
            },
            temperature = 0.2
        };

        using var response = await httpClient.PostAsJsonAsync("chat/completions", request, cancellationToken);
        response.EnsureSuccessStatusCode();

        await using var stream = await response.Content.ReadAsStreamAsync(cancellationToken);
        using var document = await JsonDocument.ParseAsync(stream, cancellationToken: cancellationToken);

        var content = document.RootElement
            .GetProperty("choices")[0]
            .GetProperty("message")
            .GetProperty("content")
            .GetString();

        return string.IsNullOrWhiteSpace(content) ? "No response generated." : content;
    }
}
