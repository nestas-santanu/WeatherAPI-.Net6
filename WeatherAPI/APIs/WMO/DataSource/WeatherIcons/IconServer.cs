using Microsoft.Extensions.Options;

namespace WeatherAPI.APIs.WMO.DataSource.WeatherIcons
{
    public class IconServer : Weather.DataProvider.WMO.Resource.WeatherIcons.IIconServer
    {
        private readonly Configuration.WeatherIcons.Server server;

        public IconServer(IOptions<Configuration.WeatherIcons.Server> options)
        {
            server = options.Value;
        }

        public string WMOIconUrl => server.WMOIconUrl;
        public string SVGIconUrl => server.SVGIconUrl;
        public string ServerUrl => server.ServerUrl;
    }
}