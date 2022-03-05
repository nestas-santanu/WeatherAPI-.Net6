using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using WeatherAPI.APIs.WMO.Response.AppEndpoints;

namespace WeatherAPI.APIs.WMO.Response.Models
{
    public class WMOValidationProblemDetails : ValidationProblemDetails
    {
        public WMOEndpointsDTO Endpoints { get; set; } = null!;

        public WMOValidationProblemDetails(
            ModelStateDictionary modelState,
            HttpContext context,
            IAppEndpoints endpoints
            ) : base(modelState)
        {
            Type = "https://httpstatuses.com/400";
            Detail = "Please correct the errors and try again.";
            Instance = context.Request.Path;
            Endpoints = new WMOEndpointsDTO
            {
                Countries = endpoints.GetCountries(context),
                SearchCity = endpoints.SearchCities(context, "value")
            };
        }
    }
}