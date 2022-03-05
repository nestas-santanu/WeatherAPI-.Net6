using System.Net;

namespace Weather.HTTPService.Service.DTO
{
    public class HttpResponseValues
    {
        public bool Success { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public string ResultAsString { get; set; } = String.Empty;
    }
}