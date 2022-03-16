using log4net;
using System.Net;
using Weather.HTTPService.Service.DTO;

namespace Weather.HTTPService
{
    public class Request : Weather.HTTPService.Service.IRequest
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(Request));

        private readonly IHttpClientFactory _httpClientFactory;

        public Request(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<HttpResponseValues> ExecuteRequestAsync(string request, string method)
        {
            HttpResponseValues responseValues;

            try
            {
                ServicePointManager.SecurityProtocol
                    = SecurityProtocolType.Tls11
                    | SecurityProtocolType.Tls12
                    | SecurityProtocolType.Tls13;

                using HttpRequestMessage httpRequestMessage = new();
                httpRequestMessage.Method = method.GetHttpMethod();
                httpRequestMessage.RequestUri = new Uri(request);

                var httpClient = _httpClientFactory.CreateClient();
                using HttpResponseMessage response
                    = await httpClient.SendAsync(httpRequestMessage).ConfigureAwait(false);

                string result
                    = await response
                        .Content
                        .ReadAsStringAsync()
                        .ConfigureAwait(false);

                responseValues = new HttpResponseValues
                {
                    Success = response.IsSuccessStatusCode,
                    StatusCode = response.StatusCode,
                    ResultAsString = result
                };
            }
            catch (Exception e)
            {
                log.Error($"Request: {request}, method: {method}", e);

                responseValues = new HttpResponseValues
                {
                    Success = false,
                    StatusCode = HttpStatusCode.InternalServerError,
                    ResultAsString = ""
                };
            }

            return responseValues;
        }
    }
}