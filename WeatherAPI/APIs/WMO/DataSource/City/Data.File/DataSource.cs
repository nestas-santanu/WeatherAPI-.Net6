using Microsoft.Extensions.Options;
using Weather.DataProvider.WMO.Resource.Cities.Data.File;

namespace WeatherAPI.APIs.WMO.DataSource.City.Data.File
{
    public class DataSource : IDataSource
    {
        private readonly Configuration.Cities.Data.File.Data resource;

        public DataSource(IOptions<Configuration.Cities.Data.File.Data> options)
        {
            resource = options.Value;
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