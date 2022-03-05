namespace WeatherAPI.APIs.WMO.Configuration
{
    public class Organization
    {
        public const string Path = "WeatherDataProvider:WMO:Organization";

        public string Name { get; set; } = String.Empty;
        public string Website { get; set; } = String.Empty;
    }
}
