using Microsoft.AspNetCore.Mvc;
using Weather.DataProvider.WMO.Cities.Service;
using Weather.DataProvider.WMO.Resource;
using Weather.DataProvider.WMO.Service;
using WeatherAPI.APIs.WMO.Response.AppEndpoints;

namespace WeatherAPI.APIs.WMO.Controllers
{
    /// <summary>
    /// Weather forecast provided by the World Meteorological Organization
    /// Home page: https://worldweather.wmo.int/en/home.html
    /// </summary>
    [ApiController]
    [Route("api/v1/weather/wmo")]
    [Produces("application/json")]
    public class WMOWeatherController : ControllerBase
    {
        private readonly ILogger<WMOWeatherController> logger;

        private readonly Weather.DataProvider.WMO.Service.IWeather weatherService;
        private readonly Weather.DataProvider.WMO.Cities.Service.ICity cityService;

        /// <summary>
        /// to generate links embedded in the response
        /// </summary>
        private readonly WeatherAPI.APIs.WMO.Response.AppEndpoints.IAppEndpoints endpoints;

        /// <summary>
        /// acknowledgement of source of data
        /// </summary>
        private readonly Weather.DataProvider.WMO.Resource.IDataProvider acknowledgement;

        public WMOWeatherController(
            ILogger<WMOWeatherController> logger,
            IWeather weatherService,
            ICity cityService,
            IAppEndpoints endpoints,
            IDataProvider acknowledgement)
        {
            this.logger = logger;
            this.weatherService = weatherService;
            this.cityService = cityService;
            this.endpoints = endpoints;
            this.acknowledgement = acknowledgement;
        }

        /// <summary>
        /// Discover the WMO Weather API endpoints. Follow the endpoints to get the weather forecast for a city.
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Returns the API Gateway endpoints.</response>
        /// <response code="500">A server error has occured.
        /// The API will return the same schema as in response code 200. 
        /// The 'detail' element will have information about the error.
        /// </response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type= typeof(Response.Models.APIGatewayDTO))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Route("", Name = "WMOAPIGateway")]
        public IActionResult GetAPIGateway()
        {
            //the representation of the resource for this request
            Response.Models.APIGatewayDTO? response;

            try
            {
                response
                    = WMO.Response.WMOAPIGateway.CreateOkResponse(
                        Request.HttpContext,
                        endpoints,
                        acknowledgement);

                return StatusCode(StatusCodes.Status200OK, response);
            }
            catch (Exception e)
            {
                logger.LogError(e, "");

                response
                    = WMO.Response.WMOAPIGateway.CreateErrorResponse(
                        Request.HttpContext,
                        endpoints,
                        acknowledgement);

                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }

        /// <summary>
        /// Gets all countries for which weather is availble.
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Returns the collection of countries.</response>
        /// <response code="404">No countries are found.
        /// The API will return the same schema as in response code 200. However, the 'countries' element will be null.
        /// </response>
        /// <response code="500">A server error has occured.
        /// The API will return the same schema as in response code 200. However, the 'countries' element will be null.
        /// </response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type= typeof(Response.Models.CountriesDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Route("countries", Name = "WMOCountries")]
        public async Task<IActionResult> GetAllCountriesAsync()
        {
            //the representation of the resource for this request
            Response.Models.CountriesDTO? representation;

            try
            {
                List<string>? countries
                    = await cityService
                        .GetCountriesAsync()
                        .ConfigureAwait(false);

                if (countries.Count == 0)
                {
                    //create the not found response
                    representation
                        = WMO.Response.Countries.CreateNotFoundResponse(
                            Request.HttpContext,
                            endpoints,
                            acknowledgement);

                    return new NotFoundObjectResult(representation)
                    {
                        ContentTypes = { "application/problem+json" }
                    };
                }

                //create the ok response
                representation
                    = WMO.Response.Countries.CreateOkResponsee(
                        Request.HttpContext,
                        countries,
                        endpoints,
                        acknowledgement);

                return StatusCode(StatusCodes.Status200OK, representation);
            }
            catch (Exception e)
            {
                logger.LogError(e, "");

                representation
                    = WMO.Response.Countries.CreateErrorResponse(
                        Request.HttpContext,
                        endpoints,
                        acknowledgement);

                return StatusCode(StatusCodes.Status500InternalServerError, representation);
            }
        }

        /// <summary>
        /// Search a city
        /// </summary>
        /// <param name="keyword">the keyword for the search</param>
        /// <returns></returns>
        /// <response code="200">Returns the result from the search.</response>
        /// <response code="400">Validation errors: Null/empty keyword.
        /// The API will return the same schema as in response code 200. However, the 'results' element will be null.
        /// </response>
        /// <response code="404">No cities are found.
        /// The API will return the same schema as in response code 200. However, the 'results' element will be null.
        /// </response>
        /// <response code="500">A server error has occured.
        /// The API will return the same schema as in response code 200. However, the 'results' element will be null.
        /// </response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type= typeof(Response.Models.SearchResultsDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Route("cities/search", Name = "WMOSearchCity")]
        public async Task<IActionResult> SearchCityAsync(string keyword)
        {
            //the representation of the resource for this request
            Response.Models.SearchResultsDTO? representation;

            try
            {
                if (string.IsNullOrWhiteSpace(keyword))
                {
                    //create the bad request resource representation
                    representation
                        = WeatherAPI.APIs.WMO.Response.SearchResults.CreateValidationErrorResponse(
                            Request.HttpContext,
                            endpoints,
                            acknowledgement);

                    return StatusCode(StatusCodes.Status400BadRequest, representation);
                }

                var cities
                    = await cityService
                        .SearchCityAsync(keyword)
                        .ConfigureAwait(false);

                if (cities.Count == 0)
                {
                    //create the not found resource representation
                    representation
                        = WMO.Response.SearchResults.CreateNotFoundResponse(
                            Request.HttpContext,
                            keyword,
                            endpoints,
                            acknowledgement);

                    return StatusCode(StatusCodes.Status404NotFound, representation);
                }

                //create the Ok resource representation
                representation
                    = WMO.Response.SearchResults.CreateOkResponse(
                        Request.HttpContext,
                        keyword,
                        cities,
                        endpoints,
                        acknowledgement);

                return StatusCode(StatusCodes.Status200OK, representation);
            }
            catch (Exception e)
            {
                logger.LogError(e, "Keyword: {a0}", keyword);

                representation
                    = WMO.Response.SearchResults.CreateErrorResource(
                        Request.HttpContext,
                        keyword,
                        endpoints,
                        acknowledgement);

                return StatusCode(StatusCodes.Status500InternalServerError, representation);
            }
        }

        /// <summary>
        /// Returns the cities in a country
        /// </summary>
        /// <param name="country"></param>
        /// <returns></returns>
        /// <response code="200">Returns the cities in the country.</response>
        /// <response code="400">Validation errors: Null/empty country.
        /// The API will return the same schema as in response code 200. However, the 'cities' element will be null.
        /// </response>
        /// <response code="404">No cities are found.
        /// The API will return the same schema as in response code 200. However, the 'cities' element will be null.
        /// </response>
        /// <response code="500">A server error has occured.
        /// The API will return the same schema as in response code 200. However, the 'cities' element will be null.
        /// </response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type= typeof(Response.Models.CitiesDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Route("countries/{country}/cities", Name = "WMOCitiesByCountry")]
        public async Task<IActionResult> GetCitiesByCountryAsync(string country)
        {
            //the representation of the resource for this request
            Response.Models.CitiesDTO? representation;

            try
            {
                if (!ModelState.IsValid)
                {
                    //create the validation error response
                    representation
                        = WMO.Response.Cities.CreateValidationErrorResponse(
                            Request.HttpContext,
                            endpoints,
                            acknowledgement);

                    return new BadRequestObjectResult(representation)
                    {
                        ContentTypes = { "application/problem+json" }
                    };
                }

                var cities
                    = await cityService
                        .GetCitiesAsync(country)
                        .ConfigureAwait(false);

                if (cities.Count == 0)
                {
                    //create the not found response
                    representation
                        = WMO.Response.Cities.CreateNotFoundResponse(
                            Request.HttpContext,
                            country,
                            endpoints,
                            acknowledgement);


                    return new NotFoundObjectResult(representation)
                    {
                        ContentTypes = { "application/problem+json" }
                    };
                }

                //create the Ok response
                representation
                    = WMO.Response.Cities.CreateOkResponse(
                        Request.HttpContext,
                        country,
                        cities,
                        endpoints,
                        acknowledgement);

                return StatusCode(StatusCodes.Status200OK, representation);
            }
            catch (Exception e)
            {
                ////this does not pass CA2254
                //logger.LogError(e, $"Country: {country}");

                ////this does not pass CA2253
                //logger.LogError(e, "Country: {0}", country);

                //this is ok
                logger.LogError(e, "Country: {a0}", country);

                //throw new Response.Models.WMOException(
                //    message: e.Message,
                //    context: Request.HttpContext,
                //    endpoints: endpoints);

                representation
                    = WMO.Response.Cities.CreateErrorResponse(
                        Request.HttpContext,
                        country,
                        endpoints,
                        acknowledgement);

                return StatusCode(StatusCodes.Status500InternalServerError, representation);
            }
        }

        /// <summary>
        /// Gets the weather for a city.
        /// </summary>
        /// <param name="country">The country which contains the city</param>
        /// <param name="city">The city for which the weather is required</param>
        /// <returns></returns>
        /// <response code="200">The weather for the city.</response>
        /// <response code="400">Validation errors: Null/empty counrty and/or city inputs.
        /// The API will return the same schema as in response code 200. However, the 'weather' element will be null.
        /// </response>
        /// <response code="404">No weather information is available.
        /// Many cities with a valid country and city inputs may not have weather information avaialble.
        /// The API will return the same schema as in response code 200. However, the 'weather' element will be null.
        /// </response>
        /// <response code="500">A server error has occured.
        /// The API will return the same schema as in response code 200. However, the 'weather' element will be null.
        /// </response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type= typeof(Response.Models.WeatherDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        //[ProducesResponseType(
        //    StatusCodes.Status400BadRequest,
        //    Type = typeof(Response.Models.WMOValidationProblemDetails))]
        //[ProducesResponseType(
        //    StatusCodes.Status404NotFound,
        //    Type = typeof(Response.Models.WMONotFoundProblemDetails))]
        //[ProducesResponseType(
        //    StatusCodes.Status500InternalServerError,
        //    Type = typeof(Response.Models.WMOExceptionProblemDetails))]
        [Route("countries/{country}/cities/{city}", Name = "WMOWeatherByCountryAndCity")]
        public async Task<IActionResult> GetWMOWeatherByCountryAndCityAsync(string country, string city)
        {
            //the representation of the resource for this request
            Response.Models.WeatherDTO? representation;
            string message;

            try
            {
                //if (!ModelState.IsValid)
                //{
                //    var problemDetails
                //        = new Response.Models.WMOValidationProblemDetails(
                //            ModelState,
                //            Request.HttpContext,
                //            endpoints);

                //    return new BadRequestObjectResult(problemDetails)
                //    {
                //        ContentTypes = { "application/problem+json" }
                //    };
                //}

                if (!ModelState.IsValid)
                {
                    message = "Both country and city are required.";
                    representation
                        = WMO.Response.CityWeather.CreateValidationErrorResponse(
                            Request.HttpContext,
                            message,
                            endpoints,
                            acknowledgement);

                    return new BadRequestObjectResult(representation)
                    {
                        ContentTypes = { "application/problem+json" }
                    };
                }

                Weather.DataProvider.WMO.Service.DTO.Weather? weather
                    = await weatherService
                        .GetWeatherAsync(country, city)
                        .ConfigureAwait(false);

                if (!weather.CurrentCondition.IsDataAvailable && !weather.Forecast.IsDataAvailable)
                {
                    ////create the not found resource representation
                    //var notFoundRepresentation
                    //    = new Response.Models.WMONotFoundProblemDetails(
                    //        message: $"Weather information is not availble for {city}, {country}",
                    //        context: Request.HttpContext,
                    //        endpoints: endpoints);

                    //return new NotFoundObjectResult(notFoundRepresentation)
                    //{
                    //    ContentTypes = { "application/problem+json" }
                    //};

                    message = $"Weather information is not availble for {city}, {country}.";
                    representation
                        = WMO.Response.CityWeather.CreateNotFoundResponsee(
                            Request.HttpContext,
                            message,
                            endpoints,
                            acknowledgement);

                    return new NotFoundObjectResult(representation)
                    {
                        ContentTypes = { "application/problem+json" }
                    };
                }

                //create the Ok response
                message = $"Weather in {city}, {country}.";
                representation
                    = WMO.Response.CityWeather.CreateOkResponse(
                        Request.HttpContext,
                        message,
                        weather,
                        endpoints,
                        acknowledgement);

                return StatusCode(StatusCodes.Status200OK, representation);
            }
            catch (Exception e)
            {
                logger.LogError(e, "Country: {a0}, City: {a1}", country, city);

                ////return the error representation
                //throw new Response.Models.WMOException(
                //    message: e.Message,
                //    context: Request.HttpContext,
                //    endpoints: endpoints);

                message = "An unexpected error occurred. You can try again. If the issue persists, please report the error.";
                representation
                    = WMO.Response.CityWeather.CreateErrorResponse(
                        Request.HttpContext,
                        message,
                        endpoints,
                        acknowledgement);

                return StatusCode(StatusCodes.Status500InternalServerError, representation);
            }
        }

        /// <summary>
        /// Gets the weather for a city.
        /// This assumes that the consumer of this API knows
        /// the Id for the city provided by the World Meteorological Organization.
        /// </summary>
        /// <param name="cityId">The id of the city as provided by the World Meteorological Organization</param>
        /// <returns></returns>
        /// <response code="200">The weather for the city.</response>
        /// <response code="404">No weather information is available.
        /// Many cities with a valid cityId may not have weather information avaialble.
        /// The API will return the same schema as in response code 200. However, the 'weather' element will be null.
        /// </response>
        /// <response code="500">A server error has occured.
        /// The API will return the same schema as in response code 200. However, the 'weather' element will be null.
        /// </response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type= typeof(Response.Models.WeatherDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        //[ProducesResponseType(
        //    StatusCodes.Status404NotFound,
        //    Type = typeof(Response.Models.WMONotFoundProblemDetails))]
        //[ProducesResponseType(
        //    StatusCodes.Status500InternalServerError,
        //    Type = typeof(Response.Models.WMOExceptionProblemDetails))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Route("{cityId:int}", Name = "WMOWeatherByCityId")]
        public async Task<IActionResult> GetWMOWeatherByCityIdAsync(int cityId)
        {
            //the representation of the resource for this request
            WeatherAPI.APIs.WMO.Response.Models.WeatherDTO? representation;
            string message;

            try
            {
                Weather.DataProvider.WMO.Service.DTO.Weather weather
                    = await weatherService
                        .GetWeatherAsync(cityId)
                        .ConfigureAwait(false);

                if (!weather.CurrentCondition.IsDataAvailable && !weather.Forecast.IsDataAvailable)
                {
                    ////create the not found resource representation
                    //var notFoundRepresentation
                    //    = new Response.Models.WMONotFoundProblemDetails(
                    //        message: $"Weather information is not availble for city with Id: {cityId}.",
                    //        context: Request.HttpContext,
                    //        endpoints: endpoints);

                    //return new NotFoundObjectResult(notFoundRepresentation)
                    //{
                    //    ContentTypes = { "application/problem+json" }
                    //};

                    message = $"Weather information is not availble for city with Id: {cityId}.";
                    representation
                        = WMO.Response.CityWeather.CreateNotFoundResponsee(
                            Request.HttpContext,
                            message,
                            endpoints,
                            acknowledgement);

                    return new NotFoundObjectResult(representation)
                    {
                        ContentTypes = { "application/problem+json" }
                    };
                }

                //create the Ok response
                message = $"Weather in city with Id: {cityId}.";
                representation
                    = WMO.Response.CityWeather.CreateOkResponse(
                        Request.HttpContext,
                        message,
                        weather,
                        endpoints,
                        acknowledgement);

                return StatusCode(StatusCodes.Status200OK, representation);
            }
            catch (Exception e)
            {
                logger.LogError(e, "CityId: {a0}", cityId);

                ////return the error representation
                //throw new Response.Models.WMOException(
                //    message: e.Message,
                //    context: Request.HttpContext,
                //    endpoints: endpoints);

                message = "An unexpected error occurred. You can try again. If the issue persists, please report the error.";
                representation
                    = WMO.Response.CityWeather.CreateErrorResponse(
                        Request.HttpContext,
                        message,
                        endpoints,
                        acknowledgement);

                return StatusCode(StatusCodes.Status500InternalServerError, representation);
            }
        }
    }
}