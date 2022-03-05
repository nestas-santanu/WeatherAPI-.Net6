namespace WeatherAPI.APIs.WMO.Response.Models
{
    public interface IDetails
    {
        string Type { get; set; }
        string Title { get; set; }
        int Status { get; set; }
        string Detail { get; set; }
        string Instance { get; set; }
    }
}
