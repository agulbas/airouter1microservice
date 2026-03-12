using AiRouter.ItineraryAgent.Service.Agent;
using AiRouter.ItineraryAgent.Service.Tools;
using AiRouter.Shared.Contracts;
using AiRouter.Shared.LLM;
using AiRouter.Shared.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenAiClient(builder.Configuration);
builder.Services.AddScoped<AgentService>();
builder.Services.AddScoped<IAgentTool, ItineraryPlannerTool>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.MapPost("/agent/query", async (AgentQueryRequest request, AgentService service, CancellationToken cancellationToken) =>
{
    if (string.IsNullOrWhiteSpace(request.Query))
    {
        return Results.BadRequest("Query is required.");
    }

    var content = await service.ExecuteAsync(request.Query, cancellationToken);
    return Results.Ok(new AgentQueryResponse("ItineraryAgent.Service", content));
});

app.Run();
