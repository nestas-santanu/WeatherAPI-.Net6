using Microsoft.AspNetCore.Http.Extensions;
using Weather.DataProvider.WMO.Resource;
using WeatherAPI.APIs.WMO.Response.AppEndpoints;
using WeatherAPI.APIs.WMO.Response.Models;
using Weather.DataProvider.WMO.Cities.Service.DTO;

namespace WeatherAPI.APIs.WMO.Response
{
    internal static class Cities
    {
        internal static WMOResponse<CitiesDTO> CreateOkResponse(
            HttpContext context,
            string country,
            List<City> cities,
            IAppEndpoints endpoints,
            IDataProvider dataProvider)
        {
            List<CityDTO> _cities = new();
            foreach (var item in cities)
            {
                _cities.Add(new CityDTO
                {
                    Name = item.Name,
                    Endpoints = new CityEndpoints
                    {
                        Weather = new WeatherEndpoints
                        {
                            FromCityId = endpoints.GetCityWeather(context, item.Id),
                            FromCountryAndCity = endpoints.GetCityWeather(context, item.Country, item.Name)
                        }
                    }
                });
            }

            return new WMOResponse<CitiesDTO>
            {
                Type = "https://httpstatuses.com/200",
                Title = "OK",
                Status = StatusCodes.Status200OK,
                Detail = $"{cities.Count} cities found in {country}.",
                Instance = context.Request.GetDisplayUrl(),
                Content = new CitiesDTO
                {
                    Country = country,
                    Cities = _cities,
                    WMOEndpoints = endpoints.GatewayEndpoints(context),
                    DataProvider = dataProvider.Provider()
                }
            };
        }

        internal static WMOResponse<CitiesDTO> CreateNotFoundResponse(
            HttpContext context,
            string country,
            IAppEndpoints endpoints,
            IDataProvider dataProvider)
        {
            return new WMOResponse<CitiesDTO>
            {
                Type = "https://httpstatuses.com/404",
                Title = "Not Found",
                Status = StatusCodes.Status404NotFound,
                Detail = $"No cities found in {country}.",
                Instance = context.Request.GetDisplayUrl(),
                Content = new CitiesDTO
                {
                    Country = country,
                    Cities = null,
                    WMOEndpoints = endpoints.GatewayEndpoints(context),
                    DataProvider = dataProvider.Provider()
                }
            };
        }

        internal static WMOResponse<CitiesDTO> CreateValidationErrorResponse(
            HttpContext context,
            IAppEndpoints endpoints,
            IDataProvider dataProvider)
        {
            return new WMOResponse<CitiesDTO>
            {
                Type = "https://httpstatuses.com/400",
                Title = "One or more validation errors ocuured.",
                Status = StatusCodes.Status400BadRequest,
                Detail = "The country must be specified.",
                Instance = context.Request.GetDisplayUrl(),
                Content = new CitiesDTO
                {
                    Country = null,
                    Cities = null,
                    WMOEndpoints = endpoints.GatewayEndpoints(context),
                    DataProvider = dataProvider.Provider()
                }
            };
        }

        internal static WMOResponse<CitiesDTO> CreateErrorResponse(
            HttpContext context,
            string country,
            IAppEndpoints endpoints,
            IDataProvider dataProvider)
        {
            return new WMOResponse<CitiesDTO>
            {
                Type = "https://httpstatuses.com/500",
                Title = "An unexpected error occurred.",
                Status = StatusCodes.Status500InternalServerError,
                Detail = $"An error occurred obtaining cities for {country}. " +
                "You may try again. If the issue persists, please report the error.",
                Instance = context.Request.GetDisplayUrl(),
                Content = new CitiesDTO
                {
                    Country = country,
                    Cities = null,
                    WMOEndpoints = endpoints.GatewayEndpoints(context),
                    DataProvider = dataProvider.Provider()
                }
            };
        }
    }
}