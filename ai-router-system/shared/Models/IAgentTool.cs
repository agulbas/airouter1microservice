namespace AiRouter.Shared.Models;

public interface IAgentTool
{
    string Name { get; }
    Task<string> Execute(string query, CancellationToken cancellationToken = default);
}
