namespace WeatherAPI.APIs.WMO.Response.Models
{
    public class EndpointDTO
    {
        public string Url { set; get; } = null!;
        public string Method { set; get; } = null!;
        public string? Description { set; get; }
    }
}