using AiRouter.Shared.Models;

namespace AiRouter.ItineraryAgent.Service.Tools;

public sealed class ItineraryPlannerTool : IAgentTool
{
    public string Name => "ItineraryPlannerTool";

    public Task<string> Execute(string query, CancellationToken cancellationToken = default)
    {
        // Replace this placeholder with real integrations (GDS APIs, maps APIs, etc.) in production.
        return Task.FromResult("Simulated itinerary outline for query: '" + query + "'.");
    }
}
