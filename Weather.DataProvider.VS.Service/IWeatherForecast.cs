namespace Weather.DataProvider.VS.Service
{
    public interface IWeatherForecast
    {
        IEnumerable<DTO.WeatherForecast> GetWeatherForecast();
    }
}