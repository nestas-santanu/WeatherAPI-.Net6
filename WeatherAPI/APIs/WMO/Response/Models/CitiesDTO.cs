namespace WeatherAPI.APIs.WMO.Response.Models
{
    public class CitiesDTO
    {
        public string? Country { get; set; }
        public List<CityDTO>? Cities { get; set; }
        public WMOEndpointsDTO WMOEndpoints { get; set; } = null!;
        public DataProviderDTO? DataProvider { get; set; }
    }
}