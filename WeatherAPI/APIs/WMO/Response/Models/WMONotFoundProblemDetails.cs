using Microsoft.AspNetCore.Mvc;
using WeatherAPI.APIs.WMO.Response.AppEndpoints;

namespace WeatherAPI.APIs.WMO.Response.Models
{
    public class WMONotFoundProblemDetails : ProblemDetails
    {
        public WMOEndpointsDTO Endpoints { get; set; } = null!;

        public WMONotFoundProblemDetails(
            string message,
            HttpContext context,
            IAppEndpoints endpoints
            )
        {
            Type = "https://httpstatuses.com/404";
            Title = "Not Found";
            Status = StatusCodes.Status404NotFound;
            Detail = message;
            Instance = context.Request.Path;
            Endpoints = new WMOEndpointsDTO
            {
                Countries = endpoints.GetCountries(context),
                SearchCity = endpoints.SearchCities(context, "value")
            };
        }
    }
}