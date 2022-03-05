using Microsoft.AspNetCore.Http.Extensions;
using Weather.DataProvider.WMO.Resource;
using WeatherAPI.APIs.WMO.Response.AppEndpoints;
using WeatherAPI.APIs.WMO.Response.Models;

namespace WeatherAPI.APIs.WMO.Response
{
    internal static class Countries
    {
        internal static WMOResponse<CountriesDTO> CreateOkResponsee(
            HttpContext context,
            List<string> countries,
            IAppEndpoints endpoints,
            IDataProvider dataProvider)
        {
            List<Country> _countries = new();

            foreach (var item in countries)
            {
                _countries.Add(new Country
                {
                    Name = item,
                    Endpoints = new CountryEndpoints
                    {
                        Cities = endpoints.GetCities(context, item),
                    }
                });
            }

            return new WMOResponse<CountriesDTO>
            {
                Type = "https://httpstatuses.com/200",
                Title = "OK",
                Status = StatusCodes.Status200OK,
                Detail = $"{countries.Count} countries found.",
                Instance = context.Request.GetDisplayUrl(),
                Content = new CountriesDTO
                {
                    Countries = _countries,
                    WMOEndpoints = endpoints.GatewayEndpoints(context),
                    DataProvider = dataProvider.Provider()
                }
            };
        }

        internal static WMOResponse<CountriesDTO> CreateNotFoundResponse(
            HttpContext context,
            IAppEndpoints endpoints,
            IDataProvider dataProvider)
        {
            return new WMOResponse<CountriesDTO>
            {
                Type = "https://httpstatuses.com/404",
                Title = "Not Found",
                Status = StatusCodes.Status404NotFound,
                Detail = "No countries were found.",
                Instance = context.Request.GetDisplayUrl(),
                Content = new CountriesDTO
                {
                    Countries = null,
                    WMOEndpoints = endpoints.GatewayEndpoints(context),
                    DataProvider = dataProvider.Provider()
                }
            };
        }

        internal static WMOResponse<CountriesDTO> CreateErrorResponse(
            HttpContext context,
            IAppEndpoints endpoints,
            IDataProvider dataProvider)
        {
            return new WMOResponse<CountriesDTO>
            {
                Type = "https://httpstatuses.com/500",
                Title = "An unexpected error occurred.",
                Status = StatusCodes.Status500InternalServerError,
                Detail = "An error occurred obtaining countries. " +
                "You may try again. If the issue persists, please report the error.",
                Instance = context.Request.GetDisplayUrl(),
                Content = new CountriesDTO
                {
                    Countries = null,
                    WMOEndpoints = endpoints.GatewayEndpoints(context),
                    DataProvider = dataProvider.Provider()
                }
            };
        }
    }
}