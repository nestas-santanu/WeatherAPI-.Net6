namespace Weather.DataProvider.WMO.Data.WeatherIcons
{
    public interface IWeatherIcon
    {
        Task<DTO.WeatherIcon?> GetWeatherIconAsync(int iconId);
    }
}