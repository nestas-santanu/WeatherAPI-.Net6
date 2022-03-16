using Microsoft.Extensions.Options;
using Weather.DataProvider.WMO.Resource.WeatherIcons.Data.File;

namespace WeatherAPI.APIs.WMO.DataSource.WeatherIcons.Data.File
{
    public class DataSource : IDataSource
    {
        private readonly Configuration.WeatherIcons.Data.File.Data resource;

        public DataSource(IOptions<Configuration.WeatherIcons.Data.File.Data> options)
        {
            resource = options.Value;
        }

        public string? FilePath => GetWeatherIconsDataFile();

        private string? GetWeatherIconsDataFile()
        {
            string? contentRootPath = AppDomain.CurrentDomain.GetData("ContentRootPath") as string;

            if (string.IsNullOrWhiteSpace(contentRootPath))
            {
                return null;
            }

            return Path.Combine(contentRootPath, resource.Location, resource.Filename);
        }
    }
}