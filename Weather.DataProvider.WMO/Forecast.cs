using log4net;
using Newtonsoft.Json;
using Weather.DataProvider.WMO.Data.WeatherIcons;
using Weather.DataProvider.WMO.DTO;
using Weather.DataProvider.WMO.Resource;
using Weather.DataProvider.WMO.Service.DTO;
using Weather.HTTPService.Service;
using Weather.HTTPService.Service.DTO;

namespace Weather.DataProvider.WMO
{
    public class Forecast : Weather.DataProvider.WMO.Service.IForecast
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(Forecast));

        private readonly Weather.DataProvider.WMO.Resource.IForecast forecastService;
        private readonly Weather.HTTPService.Service.IRequest httpService;
        private readonly Weather.DataProvider.WMO.Data.WeatherIcons.IWeatherIcon weatherIconService;

        public Forecast(
            IForecast forecastService,
            IRequest httpService,
            IWeatherIcon weatherIconService)
        {
            this.forecastService = forecastService;
            this.httpService = httpService;
            this.weatherIconService = weatherIconService;
        }

        public async Task<Response> GetForecastDataAsync(int cityId)
        {
            try
            {
                string urlRaw = forecastService.Url;
                if (string.IsNullOrWhiteSpace(urlRaw))
                {
                    throw new Exception("The weather forecast Url is not available.");
                }

                string url = urlRaw.Replace("[City ID]", cityId.ToString());

                HttpResponseValues? response
                    = await httpService
                        .ExecuteRequestAsync(url, forecastService.Method)
                        .ConfigureAwait(false);

                if (response == null)
                {
                    throw new Exception("Could not obtain a response from the weather forecast service.");
                }

                switch (response.StatusCode)
                {
                    case System.Net.HttpStatusCode.OK:
                        break;

                    case System.Net.HttpStatusCode.NotFound:

                        return new Response
                        {
                            StatusCode = (int)response.StatusCode,
                            Message = $"Forecast not available for city with Id: {cityId}.",
                            Forecast = new Service.DTO.ForecastValues
                            {
                                DateIssued = null,
                                Timezone = "",
                                Forecasts = new List<DailyForecast>()
                            }
                        };

                    default:
                        log.Error("Error: " +
                            "Source: Weather.DataProvider.WMO.Forecast, " +
                            "Operation: GetForecastDataAsync.ExecuteRequestAsync, " +
                            $"StatusCode: {response.StatusCode}, " +
                            $"Result: {response.ResultAsString}, " +
                            $"CityId: {cityId}");

                        throw new Exception("An error occurred obtaining the weather forecast.");
                }

                DTO.ForecastData? data
                    = JsonConvert.DeserializeObject<DTO.ForecastData>(response.ResultAsString);

                if (data == null)
                {
                    throw new Exception($"The weather forecast for City Id: {cityId} is not available.");
                }

                if (data.City?.Forecast == null)
                {
                    return new Response
                    {
                        StatusCode = (int)System.Net.HttpStatusCode.NotFound,
                        Message = $"Forecast not available for city with Id: {cityId}.",
                        Forecast = new Service.DTO.ForecastValues
                        {
                            DateIssued = null,
                            Timezone = "",
                            Forecasts = new List<DailyForecast>()
                        }
                    };
                }

                //forecasts are not available for many cities, example: Baroda, India; all cities in Bangladesh
                //these should be returned as a 404 Not Found
                var forecasts
                    = await GetDailyForecastsAsync(data.City.Forecast.ForecastDay).ConfigureAwait(false);

                if (forecasts.Count == 0)
                {
                    return new Response
                    {
                        StatusCode = (int)System.Net.HttpStatusCode.NotFound,
                        Message = $"Forecast not available for city with Id: {cityId}.",
                        Forecast = new Service.DTO.ForecastValues
                        {
                            DateIssued = null,
                            Timezone = "",
                            Forecasts = new List<DailyForecast>()
                        }
                    };
                }

                return new Response
                {
                    StatusCode = (int)System.Net.HttpStatusCode.OK,
                    Message = $"Forecast for city with Id: {cityId}.",
                    Forecast = new Service.DTO.ForecastValues
                    {
                        DateIssued = GetDate(data.City.Forecast.IssueDate),
                        Timezone = data.City.Forecast.TimeZone,
                        Forecasts = forecasts
                    }
                };
            }
            catch (Exception e)
            {
                log.Error($"CityId: {cityId}", e);

                throw;
            }
        }

        private static DateTime? GetDate(string issueDate)
        {
            bool b = DateTime.TryParse(issueDate, out DateTime forecastDate);

            if (b)
            {
                return forecastDate;
            }

            return null;
        }

        private async Task<List<DailyForecast>> GetDailyForecastsAsync(List<DTO.ForecastDay> forecastDays)
        {
            List<DailyForecast> dailyForecasts = new();

            foreach (var item in forecastDays)
            {
                dailyForecasts.Add(new DailyForecast
                {
                    ForecastDate = item.ForecastDate,
                    Temperature = new Temperature
                    {
                        Celcius = new TemperatureValues
                        {
                            MinValue = item.MinTemp,
                            MaxValue = item.MaxTemp
                        },
                        Fahrenheit = new TemperatureValues
                        {
                            MinValue = item.MinTempF,
                            MaxValue = item.MaxTempF
                        }
                    },
                    Condition = new WeatherCondition
                    {
                        Description = item.Weather,
                        Icon = await GetWeatherIconAsync(item.WeatherIcon).ConfigureAwait(false)
                    }
                });
            }

            return dailyForecasts;
        }

        private async Task<WeatherIcon> GetWeatherIconAsync(int iconId)
        {
            try
            {
                Data.WeatherIcons.DTO.WeatherIcon? icon
                    = await weatherIconService.GetWeatherIconAsync(iconId).ConfigureAwait(false);

                if (icon == null)
                {
                    return new WeatherIcon
                    {
                        WMOIcon = "",
                        AltIcon = "",
                        Server = ""
                    };
                }

                return new WeatherIcon
                {
                    WMOIcon = icon.Icon.WMOIcon,
                    AltIcon = icon.Icon.AltIcon,
                    Server = icon.IconServer
                };
            }
            catch (Exception)
            {
                return new WeatherIcon
                {
                    WMOIcon = "",
                    AltIcon = "",
                    Server = ""
                };
            }
        }
    }
}