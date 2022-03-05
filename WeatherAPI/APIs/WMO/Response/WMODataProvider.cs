using Weather.DataProvider.WMO.Resource;
using WeatherAPI.APIs.WMO.Response.Models;

namespace WeatherAPI.APIs.WMO.Response
{
    public static class WMODataProvider
    {
        public static DataProviderDTO Provider(this IDataProvider dataProvider)
        {
            return new DataProviderDTO
            {
                Name = dataProvider.Name,
                Website = dataProvider.Website
            };
        }

    }
}
