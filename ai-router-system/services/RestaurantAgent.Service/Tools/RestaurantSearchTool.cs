using AiRouter.Shared.Models;

namespace AiRouter.RestaurantAgent.Service.Tools;

public sealed class RestaurantSearchTool : IAgentTool
{
    public string Name => "RestaurantSearchTool";

    public Task<string> Execute(string query, CancellationToken cancellationToken = default)
    {
        // Replace this placeholder with real integrations (GDS APIs, maps APIs, etc.) in production.
        return Task.FromResult("Simulated restaurant recommendations for query: '" + query + "'.");
    }
}
