namespace WeatherAPI.APIs.WMO.Response.Models
{
    public class SearchResultsDTO
    {
        public string Keyword { get; set; } = string.Empty;
        public List<Result>? Results { get; set; }
        public WMOEndpointsDTO WMOEndpoints { get; set; } = null!;
        public DataProviderDTO? DataProvider { get; set; }
    }

    public class Result
    {
        public string Country { get; set; } = null!;
        public List<CityDTO> Cities { get; set; } = null!;
    }
}