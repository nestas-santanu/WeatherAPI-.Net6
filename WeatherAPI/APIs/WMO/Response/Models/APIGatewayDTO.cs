namespace WeatherAPI.APIs.WMO.Response.Models
{
    public class APIGatewayDTO: IDetails
    {
        public string Type { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public int Status { get; set; }
        public string Detail { get; set; } = string.Empty;
        public string Instance { get; set; } = string.Empty;

        public WMOEndpointsDTO WMOEndpoints { get; set; } = null!;
        public DataProviderDTO? DataProvider { get; set; }
    }
}
