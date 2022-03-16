namespace Weather.DataProvider.WMO.Resource.WeatherIcons
{
    public interface IIconServer
    {
        string WMOIconUrl { get; }
        string SVGIconUrl { get; }
        string ServerUrl { get; }
    }
}