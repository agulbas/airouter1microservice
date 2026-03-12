using AiRouter.Shared.Models;

namespace AiRouter.FlightAgent.Service.Tools;

public sealed class FlightSearchTool : IAgentTool
{
    public string Name => "FlightSearchTool";

    public Task<string> Execute(string query, CancellationToken cancellationToken = default)
    {
        // Replace this placeholder with real integrations (GDS APIs, maps APIs, etc.) in production.
        return Task.FromResult("Simulated flight options for query: '" + query + "'.");
    }
}
