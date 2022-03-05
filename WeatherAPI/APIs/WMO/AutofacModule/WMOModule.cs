using Autofac;

namespace WeatherAPI.APIs.WMO.AutofacModule
{
    public class WMOModule: Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            //data provider
            builder
                .RegisterType<WeatherAPI.APIs.WMO.DataSource.DataProvider>()
                .As<Weather.DataProvider.WMO.Resource.IDataProvider>()
                .InstancePerLifetimeScope();

            //process the weather
            builder
                .RegisterType<Weather.DataProvider.WMO.CityWeather>()
                .As<Weather.DataProvider.WMO.Service.IWeather>()
                .InstancePerLifetimeScope();

            //get the data
            //forecast resource (from configuration)
            builder
                .RegisterType<WeatherAPI.APIs.WMO.DataSource.Forecast>()
                .As<Weather.DataProvider.WMO.Resource.IForecast>()
                .InstancePerLifetimeScope();

            //get the forecast forecast
            builder
                .RegisterType<Weather.DataProvider.WMO.Forecast>()
                .As<Weather.DataProvider.WMO.Service.IForecast>()
                .InstancePerLifetimeScope();

            //cities
            //this gets countries/cities
            builder
                .RegisterType<Weather.DataProvider.WMO.Cities.Cities>()
                .As<Weather.DataProvider.WMO.Cities.Service.ICity>()
                .InstancePerLifetimeScope();

            //cities are read from a file
            //this gets the file location
            builder
                .RegisterType<WeatherAPI.APIs.WMO.DataSource.City.DataSource>()
                .As<Weather.DataProvider.WMO.Cities.Data.FromFile.IDataSource>()
                .InstancePerLifetimeScope();

            //this reads from the file
            builder
                .RegisterType<Weather.DataProvider.WMO.Cities.Data.FromFile.Read>()
                .As<Weather.DataProvider.WMO.Cities.Service.Data.IRead>()
                .InstancePerLifetimeScope();

            //resource endpoints in response
            builder
                .RegisterType<WeatherAPI.APIs.WMO.Response.AppEndpoints.AppEndpoints>()
                .As<WeatherAPI.APIs.WMO.Response.AppEndpoints.IAppEndpoints>()
                .InstancePerLifetimeScope();
        }
    }
}
