using WeatherAPI.APIs.WMO.Response.Models;

namespace WeatherAPI.APIs.WMO.Response.AppEndpoints
{
    public interface IAppEndpoints
    {
        EndpointDTO? GetCities(HttpContext context, string? country);

        EndpointDTO? GetCityWeather(HttpContext context, int cityId);

        EndpointDTO? GetCityWeather(HttpContext context, string? country, string? city);

        EndpointDTO GetCountries(HttpContext context);

        EndpointDTO SearchCities(HttpContext context, string? keyword);
    }
}