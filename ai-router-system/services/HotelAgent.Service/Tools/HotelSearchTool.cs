using AiRouter.Shared.Models;

namespace AiRouter.HotelAgent.Service.Tools;

public sealed class HotelSearchTool : IAgentTool
{
    public string Name => "HotelSearchTool";

    public Task<string> Execute(string query, CancellationToken cancellationToken = default)
    {
        // Replace this placeholder with real integrations (GDS APIs, maps APIs, etc.) in production.
        return Task.FromResult("Simulated hotel recommendations for query: '" + query + "'.");
    }
}
