using AiRouter.Shared.Contracts;

namespace AiRouter.Router.Service.Routing;

public interface IAgentRouter
{
    Task<QueryResponse> RouteAsync(string query, CancellationToken cancellationToken = default);
}
