namespace WeatherAPI.APIs.WMO.Response.Models
{
    public class CityDTO
    {
        public string Name { get; set; } = null!;
        public CityEndpoints Endpoints { get; set; } = null!;
    }

    public class CityEndpoints
    {
        public WeatherEndpoints Weather { get; set; } = null!;
    }

    public class WeatherEndpoints
    {
        public EndpointDTO? FromCityId { get; set; }
        public EndpointDTO? FromCountryAndCity { get; set; }
    }
}