using Microsoft.AspNetCore.Mvc;
using Weather.DataProvider.VS.Service;
using Weather.DataProvider.VS.Service.DTO;

namespace WeatherAPI.APIs.VS.Controllers
{
    /// <summary>
    /// visual studio default weather forecast api.
    /// the 'business logic' has been moved to the project Weather.DataProvider.VS
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        //private static readonly string[] Summaries = new[]
        //{
        //    "Freezing", 
        //    "Bracing", 
        //    "Chilly", 
        //    "Cool", 
        //    "Mild", 
        //    "Warm", 
        //    "Balmy", 
        //    "Hot", 
        //    "Sweltering", 
        //    "Scorching"
        //};

        private readonly ILogger<WeatherForecastController> logger;

        private readonly Weather.DataProvider.VS.Service.IWeatherForecast weatherForecastService;

        public WeatherForecastController(
            ILogger<WeatherForecastController> logger,
            IWeatherForecast weatherForecastService)
        {
            this.logger = logger;
            this.weatherForecastService = weatherForecastService;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast>? Get()
        {
            //return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            //{
            //    Date = DateTime.Now.AddDays(index),
            //    TemperatureC = Random.Shared.Next(-20, 55),
            //    Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            //})
            //.ToArray();

            try
            {
                return weatherForecastService.GetWeatherForecast();
            }
            catch (Exception e)
            {
                logger.LogError(e, "");

                return null;
            }
        }
    }
}