using Autofac;

namespace WeatherAPI.APIs.VS.AutofacModule
{
    public class VSModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder
                    .RegisterType<Weather.DataProvider.VS.Forecast>()
                    .As<Weather.DataProvider.VS.Service.IWeatherForecast>()
                    .InstancePerLifetimeScope();
        }
    }
}