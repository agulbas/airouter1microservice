namespace AiRouter.Shared.Contracts;

public sealed record RouteQueryRequest(string Query);

public sealed record AgentQueryRequest(string Query, string Intent);

public sealed record AgentQueryResponse(string Agent, string Content);
