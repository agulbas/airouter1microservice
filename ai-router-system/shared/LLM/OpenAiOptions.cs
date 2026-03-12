namespace AiRouter.Shared.LLM;

public sealed class OpenAiOptions
{
    public const string SectionName = "OpenAI";

    public string Model { get; init; } = "gpt-4o-mini";
    public string BaseUrl { get; init; } = "https://api.openai.com/v1";
}
