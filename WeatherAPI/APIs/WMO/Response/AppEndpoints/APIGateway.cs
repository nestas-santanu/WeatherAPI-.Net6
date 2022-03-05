using WeatherAPI.APIs.WMO.Response.Models;

namespace WeatherAPI.APIs.WMO.Response.AppEndpoints
{
    public static class APIGateway
    {
        public static WMOEndpointsDTO GatewayEndpoints(
            this IAppEndpoints endpoints,
            HttpContext context)
        {
            return new WMOEndpointsDTO
            {
                Countries = endpoints.GetCountries(context),
                SearchCity = endpoints.SearchCities(context, "value")
            };
        }
    }
}