namespace AiRouter.Shared.LLM;

public interface IOpenAiClient
{
    Task<string> ClassifyIntent(string query, CancellationToken cancellationToken = default);
    Task<string> RunAgent(string systemPrompt, string userPrompt, CancellationToken cancellationToken = default);
}
