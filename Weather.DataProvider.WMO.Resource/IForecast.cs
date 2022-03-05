namespace Weather.DataProvider.WMO.Resource
{
    /// <summary>
    /// obtain the weather data from this location
    /// https://worldweather.wmo.int/en/json/[City ID]_en.json
    /// where [City ID]is city id of the city
    /// refer: https://worldweather.wmo.int/en/dataguide.html
    /// </summary>
    public interface IForecast
    {
        string Url { get; }
        string Method { get; }
    }
}