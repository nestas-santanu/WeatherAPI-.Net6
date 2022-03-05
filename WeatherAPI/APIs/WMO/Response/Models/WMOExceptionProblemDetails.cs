using Microsoft.AspNetCore.Mvc;

namespace WeatherAPI.APIs.WMO.Response.Models
{
    public class WMOExceptionProblemDetails : ProblemDetails
    {
        public WMOEndpointsDTO Endpoints { get; set; } = null!;
    }
}