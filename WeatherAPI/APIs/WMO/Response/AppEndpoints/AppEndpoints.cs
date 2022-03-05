using WeatherAPI.APIs.WMO.Response.Models;

namespace WeatherAPI.APIs.WMO.Response.AppEndpoints
{
    internal class AppEndpoints: IAppEndpoints
    {
        private readonly Microsoft.AspNetCore.Routing.LinkGenerator generator;

        public AppEndpoints(Microsoft.AspNetCore.Routing.LinkGenerator generator)
        {
            this.generator = generator;
        }

        public EndpointDTO GetCountries(HttpContext context)
        {
            string? url
                = generator.GetUriByName(context, "WMOCountries", null);

            if (string.IsNullOrWhiteSpace(url))
            {
                throw new Exception("Cannot determine counties Url.");
            }

            var endpoint = new EndpointDTO
            {
                Url = url,
                Method = "GET",
                Title = "Get all countries."
            };

            return endpoint;
        }

        public EndpointDTO? GetCities(HttpContext context, string? country)
        {
            if (string.IsNullOrWhiteSpace(country))
            {
                return null;
            }

            string? url
                = generator.GetUriByName(
                    context,
                    "WMOCitiesByCountry",
                    new { country });

            if (string.IsNullOrWhiteSpace(url))
            {
                throw new Exception("Cannot determine cities Url.");
            }

            var endpoint = new EndpointDTO
            {
                Url = url,
                Method = "GET",
                Title = $"Get cities in {country}."
            };

            return endpoint;
        }

        public EndpointDTO? GetCityWeather(HttpContext context, int cityId)
        {
            if (cityId == 0)
            {
                return null;
            }

            string? url
                = generator.GetUriByName(
                    context,
                    "WMOWeatherByCityId",
                    new { cityId });

            if (string.IsNullOrWhiteSpace(url))
            {
                throw new Exception("Cannot determine city weather Url.");
            }
            var endpoint = new EndpointDTO
            {
                Url = url,
                Method = "GET",
                Title = $"Get weather for city with Id: {cityId}. " +
                "This endpoint will not be used in production, and is for demonstration only."
            };

            return endpoint;
        }

        public EndpointDTO? GetCityWeather(HttpContext context, string? country, string? city)
        {
            if (string.IsNullOrWhiteSpace(country) || string.IsNullOrWhiteSpace(city))
            {
                return null;
            }

            string? url
                = generator.GetUriByName(
                    context,
                    "WMOWeatherByCountryAndCity",
                    new { country, city });

            if (string.IsNullOrWhiteSpace(url))
            {
                throw new Exception("Cannot determine city weather Url.");
            }

            var endpoint = new EndpointDTO
            {
                Url = url,
                Method = "GET",
                Title = $"Get weather at {city}, {country}."
            };

            return endpoint;
        }

        public EndpointDTO SearchCities(HttpContext context, string? keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword))
            {
                keyword = "keyword";
            }

            string? url
                = generator.GetUriByName(
                    context,
                    "WMOSearchCity",
                    new { keyword });

            if (string.IsNullOrWhiteSpace(url))
            {
                throw new Exception("Cannot determine search cities Url.");
            }

            var endpoint = new EndpointDTO
            {
                Url = url,
                Method = "GET",
                Title = $"Search cities by keyword."
            };

            return endpoint;
        }
    }
}