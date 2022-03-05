using WeatherAPI.APIs.WMO.Response.AppEndpoints;

namespace WeatherAPI.APIs.WMO.Response.Models
{
    public class WMOException : Exception
    {
        public string Type { get; set; }
        public string Title { get; set; }
        public int Status { get; set; }
        public string Detail { get; set; }
        public string Instance { get; set; }
        public WMOEndpointsDTO Endpoints { get; set; } = null!;

        public WMOException(string message, HttpContext context, IAppEndpoints endpoints) : base(message)
        {
            Type = "https://httpstatuses.com/500";
            Title = "An unexpected error occurred.";
            Status = StatusCodes.Status500InternalServerError;
            Detail = $"{message} You can try again. If the issue persists, please report the issue.";
            Instance = context.Request.Path;
            Endpoints = new WMOEndpointsDTO
            {
                Countries = endpoints.GetCountries(context),
                SearchCity = endpoints.SearchCities(context, "value")
            };
        }
    }
}