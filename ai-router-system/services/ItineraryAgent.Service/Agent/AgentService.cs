using AiRouter.Shared.LLM;
using AiRouter.Shared.Models;

namespace AiRouter.ItineraryAgent.Service.Agent;

public sealed class AgentService(IOpenAiClient openAiClient, IEnumerable<IAgentTool> tools)
{
    private const string SystemPrompt = "You are an itinerary planning assistant. Build practical day-by-day plans with timings and transport guidance in structured JSON.";

    public async Task<string> ExecuteAsync(string query, CancellationToken cancellationToken = default)
    {
        var toolOutputs = await Task.WhenAll(tools.Select(t => t.Execute(query, cancellationToken)));
        var toolSection = string.Join("\n", toolOutputs.Select((o, i) => $"Tool {i + 1}: {o}"));

        var userPrompt = $"User query: {query}\n\nContext from tools:\n{toolSection}";
        return await openAiClient.RunAgent(SystemPrompt, userPrompt, cancellationToken);
    }
}
