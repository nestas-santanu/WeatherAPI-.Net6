namespace WeatherAPI.APIs.WMO.Response.Models
{
    public class CitiesDTO : IDetails
    {
        public string Type { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public int Status { get; set; }
        public string Detail { get; set; } = string.Empty;
        public string Instance { get; set; } = string.Empty;

        public string? Country { get; set; }
        public List<CityDTO>? Cities { get; set; }
        public WMOEndpointsDTO WMOEndpoints { get; set; } = null!;
        public DataProviderDTO? DataProvider { get; set; }
    }
}