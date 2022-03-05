namespace WeatherAPI.APIs.WMO.Configuration
{
    public class Forecast
    {
        public const string Path = "WeatherDataProvider:WMO:Forecast";

        public string Url { get; set; } = String.Empty;
        public string Method { get; set; } = String.Empty;
    }
}