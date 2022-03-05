using log4net;
using Weather.DataProvider.VS.Service.DTO;

namespace Weather.DataProvider.VS
{
    public class Forecast : Weather.DataProvider.VS.Service.IWeatherForecast
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(Forecast));

        private static readonly string[] Summaries = new[]
{
            "Freezing",
            "Bracing",
            "Chilly",
            "Cool",
            "Mild",
            "Warm",
            "Balmy",
            "Hot",
            "Sweltering",
            "Scorching"
        };

        public IEnumerable<WeatherForecast> GetWeatherForecast()
        {
            try
            {
                return Enumerable.Range(1, 5).Select(index => new WeatherForecast
                {
                    Date = DateTime.Now.AddDays(index),
                    TemperatureC = Random.Shared.Next(-20, 55),
                    Summary = Summaries[Random.Shared.Next(Summaries.Length)]
                })
                .ToArray();
            }
            catch (Exception e)
            {
                log.Error("", e);

                throw;
            }
        }
    }
}