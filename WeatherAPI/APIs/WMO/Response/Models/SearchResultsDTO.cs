namespace WeatherAPI.APIs.WMO.Response.Models
{
    public class SearchResultsDTO : IDetails
    {
        public string Type { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public int Status { get; set; }
        public string Detail { get; set; } = string.Empty;
        public string Instance { get; set; } = string.Empty;

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