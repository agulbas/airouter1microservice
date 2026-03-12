namespace AiRouter.Router.Service.Routing;

public sealed class AgentRoutingOptions
{
    public const string SectionName = "AgentRoutes";

    public string FlightSearchUrl { get; init; } = "http://flight-agent-service:8080/agent/query";
    public string HotelSearchUrl { get; init; } = "http://hotel-agent-service:8080/agent/query";
    public string ItineraryPlanningUrl { get; init; } = "http://itinerary-agent-service:8080/agent/query";
    public string RestaurantRecommendationUrl { get; init; } = "http://restaurant-agent-service:8080/agent/query";
}
