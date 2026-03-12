namespace AiRouter.Shared.Contracts;

public sealed record QueryResponse(string Intent, string Agent, string Response, DateTimeOffset ProcessedAtUtc);
