namespace WeatherAPI.APIs.WMO.Response.Models
{
    public class WMOEndpointsDTO
    {
        /// <summary>
        /// get all countries for which weather is available
        /// </summary>
        public EndpointDTO Countries { get; set; } = null!;

        /// <summary>
        /// search a city
        /// </summary>
        public EndpointDTO SearchCity { get; set; } = null!;
    }
}