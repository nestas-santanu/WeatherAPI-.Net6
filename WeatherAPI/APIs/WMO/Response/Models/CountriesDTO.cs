namespace WeatherAPI.APIs.WMO.Response.Models
{
    public class CountriesDTO : IDetails
    {
        public string Type { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public int Status { get; set; }
        public string Detail { get; set; } = string.Empty;
        public string Instance { get; set; } = string.Empty;

        public List<Country>? Countries { get; set; }
        public WMOEndpointsDTO WMOEndpoints { get; set; } = null!;
        public DataProviderDTO? DataProvider { get; set; }
    }

    public class Country
    {
        public string Name { get; set; } = null!;
        public CountryEndpoints Endpoints { get; set; } = null!;
    }

    public class CountryEndpoints
    {
        /// <summary>
        /// Get all cities in a country
        /// </summary>
        public EndpointDTO Cities { get; set; } = null!;
    }
}