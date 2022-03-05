using Weather.DataProvider.WMO.Resource;
using WeatherAPI.APIs.WMO.Response.AppEndpoints;
using WeatherAPI.APIs.WMO.Response.Models;
using Weather.DataProvider.WMO.Cities.Service.DTO;
using Microsoft.AspNetCore.Http.Extensions;

namespace WeatherAPI.APIs.WMO.Response
{
    internal static class SearchResults
    {
        internal static WMOResponse<SearchResultsDTO> CreateOkResponse(
            HttpContext context,
            string keyword,
            List<City> results,
            IAppEndpoints endpoints,
            IDataProvider dataProvider)
        {
            List<Result> _results = new();

            IEnumerable<IGrouping<string, City>>? groupByCountry
                = results.GroupBy(g => g.Country);

            foreach (var group in groupByCountry)
            {
                _results.Add(new Result
                {
                    Country = group.Key,
                    Cities = GetCities(group, context, endpoints)
                });
            }

            return new WMOResponse<SearchResultsDTO>
            {
                Type = "https://httpstatuses.com/200",
                Title = "OK",
                Status = StatusCodes.Status200OK,
                Detail = $"{results.Count} cities found.",
                Instance = context.Request.GetDisplayUrl(),
                Content = new SearchResultsDTO
                {
                    Keyword = keyword,
                    Results = _results,
                    WMOEndpoints = endpoints.GatewayEndpoints(context),
                    DataProvider = dataProvider.Provider()
                }
            };
        }

        private static List<CityDTO> GetCities(
            IGrouping<string, City> group,
            HttpContext context,
            IAppEndpoints endpoints)
        {
            List<CityDTO> cities = new();

            foreach (var item in group)
            {
                cities.Add(new CityDTO
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

            return cities;
        }

        internal static WMOResponse<SearchResultsDTO> CreateNotFoundResponse(
            HttpContext context,
            string keyword,
            IAppEndpoints endpoints,
            IDataProvider dataProvider)
        {
            return new WMOResponse<SearchResultsDTO>
            {
                Type = "https://httpstatuses.com/404",
                Title = "Not Found",
                Status = StatusCodes.Status404NotFound,
                Detail = $"No cities were found for keyword: {keyword}",
                Instance = context.Request.GetDisplayUrl(),
                Content = new SearchResultsDTO
                {
                    Keyword = keyword,
                    Results = null,
                    WMOEndpoints = endpoints.GatewayEndpoints(context),
                    DataProvider = dataProvider.Provider()
                }
            };
        }

        internal static WMOResponse<SearchResultsDTO> CreateValidationErrorResponse(
            HttpContext context,
            IAppEndpoints endpoints,
            IDataProvider dataProvider)
        {
            return new WMOResponse<SearchResultsDTO>
            {
                Type = "https://httpstatuses.com/400",
                Title = "One or more validation errors ocuured.",
                Status = StatusCodes.Status400BadRequest,
                Detail = "A keyword for search must be specified.",
                Instance = context.Request.GetDisplayUrl(),
                Content = new SearchResultsDTO
                {
                    Keyword = "",
                    Results = null,
                    WMOEndpoints = endpoints.GatewayEndpoints(context),
                    DataProvider = dataProvider.Provider()
                }
            };
        }

        internal static WMOResponse<SearchResultsDTO> CreateErrorResource(
            HttpContext context,
            string keyword,
            IAppEndpoints endpoints,
            IDataProvider dataProvider)
        {
            return new WMOResponse<SearchResultsDTO>
            {
                Type = "https://httpstatuses.com/500",
                Title = "An unexpected error occurred.",
                Status = StatusCodes.Status500InternalServerError,
                Detail = "An error occurred searching for cities. " +
                "You may try again. If the issue persists, please report the error.",
                Instance = context.Request.GetDisplayUrl(),
                Content = new SearchResultsDTO
                {
                    Keyword = keyword,
                    Results = null,
                    WMOEndpoints = endpoints.GatewayEndpoints(context),
                    DataProvider = dataProvider.Provider()
                }
            };
        }
    }
}