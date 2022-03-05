using Weather.DataProvider.WMO.DTO;

namespace Weather.DataProvider.WMO.Service
{
    public interface IForecast
    {
        Task<Response> GetForecastDataAsync(int cityId);
    }
}