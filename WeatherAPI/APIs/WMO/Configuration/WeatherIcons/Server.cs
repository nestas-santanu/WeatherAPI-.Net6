namespace WeatherAPI.APIs.WMO.Configuration.WeatherIcons
{
    public class Server
    {
        public const string Path = "WeatherDataProvider:WMO:Icons:Server";

        public string WMOIconUrl { get; set; } = String.Empty;
        public string SVGIconUrl { get; set; } = String.Empty;
        public string ServerUrl { get; set; } = String.Empty;
    }
}
