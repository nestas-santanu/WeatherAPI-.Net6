namespace Weather.HTTPService
{
    internal static class HttpMethodExtension
    {
        public static HttpMethod GetHttpMethod(this string method)
        {
            if (string.IsNullOrWhiteSpace(method))
            {
                return HttpMethod.Get;
            }

            method = method.ToUpper();
            return method switch
            {
                "POST" => HttpMethod.Post,
                "PUT" => HttpMethod.Put,
                "PATCH" => new HttpMethod("PATCH"),
                "DELETE" => HttpMethod.Delete,
                _ => HttpMethod.Get,
            };
        }
    }
}