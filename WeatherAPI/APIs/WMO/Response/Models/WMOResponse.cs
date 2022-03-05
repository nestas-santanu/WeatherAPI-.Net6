namespace WeatherAPI.APIs.WMO.Response.Models
{
    public class WMOResponse<T>
    {
        public string Type { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public int Status { get; set; }
        public string Detail { get; set; } = string.Empty;
        public string Instance { get; set; } = string.Empty;

        public T Content { get; set; } = default!;
    }
}
