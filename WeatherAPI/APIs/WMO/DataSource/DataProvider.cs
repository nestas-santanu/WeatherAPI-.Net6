using Microsoft.Extensions.Options;
using WeatherAPI.APIs.WMO.Configuration;

namespace WeatherAPI.APIs.WMO.DataSource
{
    public class DataProvider : Weather.DataProvider.WMO.Resource.IDataProvider
    {
        private readonly Organization config;

        public DataProvider(IOptions<Organization> config)

        {
            this.config = config.Value;
        }

        public string Name => config.Name;

        public string Website => config.Website;
    }
}