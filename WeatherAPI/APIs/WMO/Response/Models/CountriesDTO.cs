namespace WeatherAPI.APIs.WMO.Response.Models
{
    public class CountriesDTO
    {
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