namespace AiRouter.Shared.Models;

public static class IntentCategory
{
    public const string FlightSearch = "flight_search";
    public const string HotelSearch = "hotel_search";
    public const string ItineraryPlanning = "itinerary_planning";
    public const string RestaurantRecommendation = "restaurant_recommendation";

    public static readonly IReadOnlySet<string> Allowed = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
    {
        FlightSearch,
        HotelSearch,
        ItineraryPlanning,
        RestaurantRecommendation
    };
}
