namespace Weather.DataProvider.WMO.Service
{
    public interface IWeather
    {
        Task<DTO.Weather> GetWeatherAsync(int cityId);

        Task<DTO.Weather> GetWeatherAsync(string country, string city);
    }
}