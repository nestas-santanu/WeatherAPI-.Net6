using Microsoft.Extensions.Options;

namespace WeatherAPI.APIs.WMO.DataSource
{
    public class Forecast : Weather.DataProvider.WMO.Resource.IForecast
    {
        private readonly Configuration.Forecast resource;

        public Forecast(IOptions<Configuration.Forecast> resource)
        {
            this.resource = resource.Value;
        }

        public string Url => resource.Url;

        public string Method => resource.Method;
    }
}