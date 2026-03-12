using AiRouter.Shared.Contracts;
using AiRouter.Shared.LLM;
using AiRouter.Shared.Models;
using Microsoft.Extensions.Options;

namespace AiRouter.Router.Service.Routing;

public sealed class AgentRouter(
    IOpenAiClient openAiClient,
    IHttpClientFactory clientFactory,
    IOptions<AgentRoutingOptions> options,
    ILogger<AgentRouter> logger) : IAgentRouter
{
    private readonly AgentRoutingOptions _routes = options.Value;

    public async Task<QueryResponse> RouteAsync(string query, CancellationToken cancellationToken = default)
    {
        var intent = await openAiClient.ClassifyIntent(query, cancellationToken);
        var url = intent switch
        {
            IntentCategory.FlightSearch => _routes.FlightSearchUrl,
            IntentCategory.HotelSearch => _routes.HotelSearchUrl,
            IntentCategory.ItineraryPlanning => _routes.ItineraryPlanningUrl,
            IntentCategory.RestaurantRecommendation => _routes.RestaurantRecommendationUrl,
            _ => _routes.ItineraryPlanningUrl
        };

        logger.LogInformation("Query classified as {Intent}. Forwarding to {Url}", intent, url);

        var client = clientFactory.CreateClient("agents");
        using var response = await client.PostAsJsonAsync(url, new AgentQueryRequest(query, intent), cancellationToken);
        response.EnsureSuccessStatusCode();

        var payload = await response.Content.ReadFromJsonAsync<AgentQueryResponse>(cancellationToken: cancellationToken)
            ?? throw new InvalidOperationException("Agent returned empty response.");

        return new QueryResponse(intent, payload.Agent, payload.Content, DateTimeOffset.UtcNow);
    }
}
