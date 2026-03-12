using AiRouter.Router.Service.Routing;
using AiRouter.Shared.Contracts;
using AiRouter.Shared.LLM;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenAiClient(builder.Configuration);
builder.Services.Configure<AgentRoutingOptions>(builder.Configuration.GetSection(AgentRoutingOptions.SectionName));
builder.Services.AddScoped<IAgentRouter, AgentRouter>();
builder.Services.AddHttpClient("agents");
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.MapPost("/route", async (RouteQueryRequest request, IAgentRouter router, ILoggerFactory loggerFactory, CancellationToken cancellationToken) =>
{
    var logger = loggerFactory.CreateLogger("Router");

    if (string.IsNullOrWhiteSpace(request.Query))
    {
        logger.LogWarning("Received empty query.");
        return Results.BadRequest("Query is required.");
    }

    try
    {
        var result = await router.RouteAsync(request.Query, cancellationToken);
        return Results.Ok(result);
    }
    catch (Exception ex)
    {
        logger.LogError(ex, "Failed to route query.");
        return Results.Problem("Routing failed.", statusCode: StatusCodes.Status500InternalServerError);
    }
});

app.Run();
