﻿using Microsoft.AspNetCore.Http.Extensions;
using Weather.DataProvider.WMO.Resource;
using WeatherAPI.APIs.WMO.Response.AppEndpoints;
using WeatherAPI.APIs.WMO.Response.Models;

namespace WeatherAPI.APIs.WMO.Response
{
    internal static class CityWeather
    {
        internal static WeatherDTO CreateOkResponse(
            HttpContext context,
            string message,
            Weather.DataProvider.WMO.Service.DTO.Weather weather,
            IAppEndpoints endpoints,
            IDataProvider dataProvider)
        {
            return new WeatherDTO
            {
                Type = "https://httpstatuses.com/200",
                Title = "OK",
                Status = StatusCodes.Status200OK,
                Detail = message,
                Instance = context.Request.GetDisplayUrl(),

                Weather = weather,
                WMOEndpoints = endpoints.GatewayEndpoints(context),
                DataProvider = dataProvider.Provider()
            };
        }

        internal static WeatherDTO? CreateNotFoundResponsee(
            HttpContext context,
            string message,
            IAppEndpoints endpoints,
            IDataProvider dataProvider)
        {
            return new WeatherDTO
            {
                Type = "https://httpstatuses.com/404",
                Title = "Not Found",
                Status = StatusCodes.Status404NotFound,
                Detail = message,
                Instance = context.Request.GetDisplayUrl(),

                Weather = null,
                WMOEndpoints = endpoints.GatewayEndpoints(context),
                DataProvider = dataProvider.Provider()
            };
        }

        internal static WeatherDTO? CreateValidationErrorResponse(
           HttpContext context,
           string message,
           IAppEndpoints endpoints,
           IDataProvider dataProvider)
        {
            return new WeatherDTO
            {
                Type = "https://httpstatuses.com/400",
                Title = "One or more validation errors ocuured.",
                Status = StatusCodes.Status400BadRequest,
                Detail = message,
                Instance = context.Request.GetDisplayUrl(),

                Weather = null,
                WMOEndpoints = endpoints.GatewayEndpoints(context),
                DataProvider = dataProvider.Provider()
            };
        }

        internal static WeatherDTO? CreateErrorResponse(
            HttpContext context,
            string message,
            IAppEndpoints endpoints,
            IDataProvider dataProvider)
        {
            return new WeatherDTO
            {
                Type = "https://httpstatuses.com/500",
                Title = "An unexpected error occurred.",
                Status = StatusCodes.Status500InternalServerError,
                Detail = message,
                Instance = context.Request.GetDisplayUrl(),

                Weather = null,
                WMOEndpoints = endpoints.GatewayEndpoints(context),
                DataProvider = dataProvider.Provider()
            };
        }
    }
}