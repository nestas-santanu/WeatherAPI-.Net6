namespace WeatherAPI.APIs.WMO.Configuration.Cities.Data.File
{
    public class Data
    {
        public const string Path = "WeatherDataProvider:WMO:Cities:File";

        public string Filename { get; set; } = String.Empty;
        public string Location { get; set; } = String.Empty;
    }
}