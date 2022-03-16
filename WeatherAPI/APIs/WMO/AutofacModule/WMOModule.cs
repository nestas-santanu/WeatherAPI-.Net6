using Autofac;

namespace WeatherAPI.APIs.WMO.AutofacModule
{
    public class WMOModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            //process the weather
            builder
                .RegisterType<Weather.DataProvider.WMO.CityWeather>()
                .As<Weather.DataProvider.WMO.Service.IWeather>()
                .InstancePerLifetimeScope();

            //get the data
            //forecast
            //this gets the wmo endpoint
            builder
                .RegisterType<WeatherAPI.APIs.WMO.DataSource.Forecast>()
                .As<Weather.DataProvider.WMO.Resource.IForecast>()
                .InstancePerLifetimeScope();

            //get the forecast
            builder
                .RegisterType<Weather.DataProvider.WMO.Forecast>()
                .As<Weather.DataProvider.WMO.Service.IForecast>()
                .InstancePerLifetimeScope();

            //cities
            //this gets countries/cities
            builder
                .RegisterType<Weather.DataProvider.WMO.Data.File.Cities.Cities>()
                .As<Weather.DataProvider.WMO.Data.Cities.ICity>()
                .InstancePerLifetimeScope();

            //cities are read from a file
            //this gets the file location
            builder
                .RegisterType<WeatherAPI.APIs.WMO.DataSource.City.Data.File.DataSource>()
                .As<Weather.DataProvider.WMO.Resource.Cities.Data.File.IDataSource>()
                .InstancePerLifetimeScope();

            //weather icons
            //this gets the weather icons
            builder
                .RegisterType<Weather.DataProvider.WMO.Data.File.WeatherIcons.Icons>()
                .As<Weather.DataProvider.WMO.Data.WeatherIcons.IWeatherIcon>()
                .InstancePerLifetimeScope();

            //weather icons are read from a file
            //this gets the file location
            builder
                .RegisterType<WeatherAPI.APIs.WMO.DataSource.WeatherIcons.Data.File.DataSource>()
                .As<Weather.DataProvider.WMO.Resource.WeatherIcons.Data.File.IDataSource>()
                .InstancePerLifetimeScope();

            //weather icons are served from a url
            //this gets the urls
            builder
                .RegisterType<WeatherAPI.APIs.WMO.DataSource.WeatherIcons.IconServer>()
                .As<Weather.DataProvider.WMO.Resource.WeatherIcons.IIconServer>()
                .InstancePerLifetimeScope();

            //others
            //data provider
            builder
                .RegisterType<WeatherAPI.APIs.WMO.DataSource.DataProvider>()
                .As<Weather.DataProvider.WMO.Resource.IDataProvider>()
                .InstancePerLifetimeScope();

            //resource endpoints in response
            builder
                .RegisterType<WeatherAPI.APIs.WMO.Response.AppEndpoints.AppEndpoints>()
                .As<WeatherAPI.APIs.WMO.Response.AppEndpoints.IAppEndpoints>()
                .InstancePerLifetimeScope();
        }
    }
}