# WeatherAPI-.Net6
A primer to build a Web API in .NET 6. Requires Visual Studio 2022. Community edition will do.

### Links

View a working version here: https://nestas-weather-api.azurewebsites.net/api/v1/weather/wmo. Use a client such a Postman to navigate through the links in the response.

The Swagger UI is here: https://nestas-weather-api.azurewebsites.net/swagger/index.html.

Weather icons are servered from here: https://lemon-forest-0b08efc10.1.azurestaticapps.net

### Motivation
To transition developers who have been building Web APIs using the .Net Framework (4-5-4.8) to .NET 6. These developers have missed the .Net Core phase. (This happens when getting a working software to market is more important than transitioning to new frameworks.)

New development will be in the .NET 6 space, and hence the primer.

### Why a Weather API?
Visual Studio creates a WeatherForecast API every time you create a new ASP.NET Core Web API project. This outputs ridiculous values, quite understandable, as the focus is on the Program.cs file and incorporating Open API 3 specifications for documentation.

But it was inspiration to create a Weather API that would also focus on 'other things' that are required to create an API. It also returns real data.

Weather data is sourced from the World Meteorological Organization.

### The Domain
An understanding of how the World Meteorological Organization (WMO) provides the data is required. This is available at https://worldweather.wmo.int/en/pilot.html.

Briefly this is what it says:

Individual countries have their own National Meteorological and Hydrological Service (NMHS), which collect weather data from weather stations established by them. For example, in India, the India Meteorological Department has 95 weather stations across the country.

In 2000, the WMO began establishing the World Weather Information Service (WWIS), to 'provide a centralized source of official weather information on the Internet'. Participating NMHSs provide data from their weather stations to the WWIS. The WWIS makes this available to the public through their website at https://worldweather.wmo.int/en/home.html.

It also provides an API by which data is available for a city. See this at https://worldweather.wmo.int/en/dataguide.html. The city is actually the location of a weather station. The city is actually the location of a weather station. The WWIS has provided every weather station (irrespective of the paricipating NMHS) with a unique City ID.

### What does the Weather API provide?
The WWIS API requires a client to have prior knowledge of the Id of a city.

The Weather API is HATEOAS driven, allowing the client to dynamically navigate the embedded links to ultimately obtain the city forecast by country and city. This removes the need for the client to know the id of the city. Internally, it uses the WWIS API to obtain the data.

The link GET /api/v1/weather/wmo is the start point of the the Weather API.
