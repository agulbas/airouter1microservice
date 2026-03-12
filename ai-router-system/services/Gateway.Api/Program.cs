using AiRouter.Shared.Contracts;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient("router", client =>
{
    client.BaseAddress = new Uri(builder.Configuration["Services:RouterBaseUrl"] ?? "http://router-service:8080");
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

// Gateway endpoint is public-facing and can be wrapped by auth middleware in production.
app.MapPost("/ai/query", async (QueryRequest request, IHttpClientFactory clientFactory, ILoggerFactory loggerFactory, CancellationToken cancellationToken) =>
{
    var logger = loggerFactory.CreateLogger("Gateway");
    logger.LogInformation("Forwarding query from gateway to router service.");

    var client = clientFactory.CreateClient("router");
    using var response = await client.PostAsJsonAsync("/route", new RouteQueryRequest(request.Query), cancellationToken);

    if (!response.IsSuccessStatusCode)
    {
        var body = await response.Content.ReadAsStringAsync(cancellationToken);
        logger.LogError("Router service failed: {StatusCode} {Body}", response.StatusCode, body);
        return Results.Problem("Failed to process request.", statusCode: StatusCodes.Status502BadGateway);
    }

    var routed = await response.Content.ReadFromJsonAsync<QueryResponse>(cancellationToken: cancellationToken);
    return routed is null ? Results.Problem("Empty response from router.", statusCode: StatusCodes.Status502BadGateway) : Results.Ok(routed);
});

app.Run();
