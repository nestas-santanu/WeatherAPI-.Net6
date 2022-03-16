namespace Weather.DataProvider.WMO.Data.WeatherIcons.DTO
{
    public class WeatherIcon
    {
        public string Description { get; set; } = null!;
        public Icon Icon { get; set; } = null!;
        public string? IconServer { get; set; }
    }

    public class Icon
    {
        public string WMOIcon { get; set; } = null!;
        public string? AltIcon { get; set; }
    }
}