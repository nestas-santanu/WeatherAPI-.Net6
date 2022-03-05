using log4net;
using Weather.DataProvider.WMO.Cities.Service;
using Weather.DataProvider.WMO.Service;

namespace Weather.DataProvider.WMO
{
    public class CityWeather : Weather.DataProvider.WMO.Service.IWeather
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(CityWeather));

        private readonly Weather.DataProvider.WMO.Cities.Service.ICity cityService;
        private readonly Weather.DataProvider.WMO.Service.IForecast forecastService;

        public CityWeather(ICity cityService, IForecast forecastService)
        {
            this.cityService = cityService;
            this.forecastService = forecastService;
        }

        public async Task<Service.DTO.Weather> GetWeatherAsync(int cityId)
        {
            try
            {
                Cities.Service.DTO.City? _city
                    = await cityService.GetCityAsync(cityId).ConfigureAwait(false);

                if (_city == null)
                {
                    return new Service.DTO.Weather
                    {
                        Country = "",
                        City = "",
                        CityId = cityId,
                        CurrentCondition = new Service.DTO.CurrentCondition(),
                        Forecast = new Service.DTO.Forecast
                        {
                            IsDataAvailable = false,
                            Values = new()
                        }
                    };
                }

                string country = _city.Country;
                string city = _city.Name;

                Service.DTO.Forecast forecast
                    = await GetForecastAsync(cityId, country, city).ConfigureAwait(false);

                return new Service.DTO.Weather
                {
                    Country = country,
                    City = city,
                    CityId = cityId,
                    CurrentCondition = new Service.DTO.CurrentCondition(),
                    Forecast = forecast
                };
            }
            catch (Exception e)
            {
                log.Error($"CityId: {cityId}", e);

                throw;
            }
        }

        public async Task<Service.DTO.Weather> GetWeatherAsync(string country, string city)
        {
            try
            {
                Cities.Service.DTO.City? _city
                    = await cityService.GetCityAsync(country, city).ConfigureAwait(false);

                if (_city == null)
                {
                    return new Service.DTO.Weather
                    {
                        Country = country,
                        City = city,
                        CityId = 0,
                        CurrentCondition = new Service.DTO.CurrentCondition(),
                        Forecast = new Service.DTO.Forecast
                        {
                            IsDataAvailable = false,
                            Values = new()
                        }
                    };
                }

                int cityId = _city.Id;

                Service.DTO.Forecast forecast
                    = await GetForecastAsync(cityId, country, city).ConfigureAwait(false);

                return new Service.DTO.Weather
                {
                    Country = country,
                    City = city,
                    CityId = _city.Id,
                    CurrentCondition = new Service.DTO.CurrentCondition(),
                    Forecast = forecast
                };
            }
            catch (Exception e)
            {
                log.Error($"Country: {country}, City: {city}", e);

                throw;
            }
        }

        private async Task<Service.DTO.Forecast> GetForecastAsync(int cityId, string country, string city)
        {
            var result
                = await forecastService.GetForecastDataAsync(cityId).ConfigureAwait(false);

            return result.StatusCode switch
            {
                200 => new Service.DTO.Forecast
                {
                    IsDataAvailable = true,
                    Values = result.Forecast
                },
                404 => new Service.DTO.Forecast
                {
                    IsDataAvailable = false,
                    Values = new()
                },
                _ => throw new Exception(result.Message),
            };
        }
    }
}