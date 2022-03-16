using LazyCache;
using log4net;
using Newtonsoft.Json;
using Weather.DataProvider.WMO.Data.WeatherIcons.DTO;
using Weather.DataProvider.WMO.Resource.WeatherIcons;
using Weather.DataProvider.WMO.Resource.WeatherIcons.Data.File;

namespace Weather.DataProvider.WMO.Data.File.WeatherIcons
{
    public class Icons : Weather.DataProvider.WMO.Data.WeatherIcons.IWeatherIcon
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(Icons));

        private readonly IAppCache cache;
        private readonly Weather.DataProvider.WMO.Resource.WeatherIcons.Data.File.IDataSource dataSource;
        private readonly Weather.DataProvider.WMO.Resource.WeatherIcons.IIconServer iconServer;

        public Icons(IAppCache cache, IDataSource dataSource, IIconServer iconServer)
        {
            this.cache = cache;
            this.dataSource = dataSource;
            this.iconServer = iconServer;
        }

        public async Task<WeatherIcon?> GetWeatherIconAsync(int iconId)
        {
            try
            {
                if (iconId == 0)
                {
                    return null;
                }

                var icons = await GetWeatherIconsAsync().ConfigureAwait(false);

                DTO.WeatherIcon? filteredIcon = icons.FirstOrDefault(x => x.Id == iconId);

                if (filteredIcon == null)
                {
                    return null;
                }

                return new WeatherIcon
                {
                    Description = filteredIcon.Title,
                    Icon = new Icon
                    {
                        WMOIcon
                        = !string.IsNullOrWhiteSpace(filteredIcon.Icon.WMO)
                            ? $"{iconServer.WMOIconUrl}/{filteredIcon.Icon.WMO}"
                            : "",
                        AltIcon
                        = !string.IsNullOrWhiteSpace(filteredIcon.Icon.Alt)
                            ? $"{iconServer.SVGIconUrl}/{filteredIcon.Icon.Alt}"
                            : "",
                    },
                    IconServer
                    = (!string.IsNullOrWhiteSpace(filteredIcon.Icon.WMO)
                        || !string.IsNullOrWhiteSpace(filteredIcon.Icon.Alt))
                        ? iconServer.ServerUrl
                        : ""
                };
            }
            catch (Exception e)
            {
                log.Error($"Iconid: {iconId}", e);

                throw;
            }
        }

        private async Task<List<DTO.WeatherIcon>> GetWeatherIconsAsync()
        {
            return await cache
                    .GetOrAddAsync(
                        "WeatherIcons",
                        async () => await ReadDataFromFileAsync().ConfigureAwait(false),
                        new TimeSpan(24, 0, 0))
                    .ConfigureAwait(false);
        }

        private async Task<List<DTO.WeatherIcon>> ReadDataFromFileAsync()
        {
            //get the file
            string? filePath = dataSource.FilePath;

            if (filePath == null)
            {
                throw new Exception("The path to the weather icons resource file could not be determined.");
            }

            //read the file
            //create the WeatherIcon object
            string? content = await System.IO.File.ReadAllTextAsync(filePath).ConfigureAwait(false);

            if (string.IsNullOrWhiteSpace(content))
            {
                throw new Exception("No content in the the weather icons resource file.");
            }
            else
            {
                return JsonConvert.DeserializeObject<List<DTO.WeatherIcon>>(content);
            }
        }
    }
}