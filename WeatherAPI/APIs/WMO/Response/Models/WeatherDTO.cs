namespace WeatherAPI.APIs.WMO.Response.Models
{
    public class WeatherDTO
    {
        public Weather.DataProvider.WMO.Service.DTO.Weather? Weather { get; set; }
        public WMOEndpointsDTO WMOEndpoints { get; set; } = null!;
        public DataProviderDTO? DataProvider { get; set; }
    }
}