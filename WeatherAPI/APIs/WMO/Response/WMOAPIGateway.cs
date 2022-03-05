using Microsoft.AspNetCore.Http.Extensions;
using Weather.DataProvider.WMO.Resource;
using WeatherAPI.APIs.WMO.Response.AppEndpoints;
using WeatherAPI.APIs.WMO.Response.Models;

namespace WeatherAPI.APIs.WMO.Response
{
    public static class WMOAPIGateway
    {
        internal static APIGatewayDTO CreateOkResponse(
            HttpContext context,
            IAppEndpoints endpoints,
            IDataProvider dataProvider)
        {
            return new APIGatewayDTO
            {
                Type = "https://httpstatuses.com/200",
                Title = "OK",
                Status = StatusCodes.Status200OK,
                Detail = "Follow the wmoEndpoints to discover and navigate other APIs.",
                Instance = context.Request.GetDisplayUrl(),

                WMOEndpoints = endpoints.GatewayEndpoints(context),
                DataProvider = dataProvider.Provider()
            };
        }

        internal static APIGatewayDTO CreateErrorResponse(
            HttpContext context,
            IAppEndpoints endpoints,
            IDataProvider dataProvider)
        {
            return new APIGatewayDTO
            {
                Type = "https://httpstatuses.com/500",
                Title = "An unexpected error occurred.",
                Status = StatusCodes.Status500InternalServerError,
                Detail = "The server may not be available or you may not have an active internet connection. " +
                "If the issue persists, please report the error.",
                Instance = context.Request.GetDisplayUrl(),

                WMOEndpoints = endpoints.GatewayEndpoints(context),
                DataProvider = dataProvider.Provider()
            };
        }
    }
}