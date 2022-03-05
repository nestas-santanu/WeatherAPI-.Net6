using Microsoft.Extensions.Options;
using WeatherAPI.APIs.WMO.Configuration;

namespace WeatherAPI.APIs.WMO.DataSource.City
{
    public class DataSource : Weather.DataProvider.WMO.Cities.Data.FromFile.IDataSource
    {
        private readonly Cities resource;

        public DataSource(IOptions<Cities> resource)
        {
            this.resource = resource.Value;
        }

        public string? FilePath => GetCitiesDataFile();

        private string? GetCitiesDataFile()
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