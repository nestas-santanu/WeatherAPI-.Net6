namespace Weather.DataProvider.WMO.Service.DTO
{
    public class Weather
    {
        public string Country { get; set; } = null!;
        public string City { get; set; } = null!;
        public int CityId { get; set; }
        public CurrentCondition CurrentCondition { get; set; } = null!;
        public Forecast Forecast { get; set; } = null!;
    }

    public class CurrentCondition
    {
        public bool IsDataAvailable { get; set; } = false;
        public CurrentValues? Values { get; set; } = new();
    }

    public class CurrentValues
    {
        public string Message { get; set; } = "This feature is not impleted yet.";
    }

    public class Forecast
    {
        public bool IsDataAvailable { get; set; }
        public ForecastValues? Values { get; set; }
    }

    public class ForecastValues
    {
        public DateTime? DateIssued { get; set; }
        public string Timezone { get; set; } = "NA";
        public List<DailyForecast> Forecasts { get; set; } = new();
    }

    public class DailyForecast
    {
        public string ForecastDate { get; set; } = string.Empty;
        public Temperature Temperature { get; set; } = new();
        public WeatherCondition Condition { get; set; } = new();
    }

    public class WeatherCondition
    {
        public string Description { get; set; } = null!;
        public WeatherIcon Icon { get; set; } = new();
    }

    public class Temperature
    {
        public TemperatureValues? Celcius { get; set; }
        public TemperatureValues? Fahrenheit { get; set; }
    }

    public class TemperatureValues
    {
        public string MinValue { get; set; } = null!;
        public string MaxValue { get; set; } = null!;
    }

    public class WeatherIcon
    {
        public string WMOIcon { get; set; } = string.Empty;
        public string? AltIcon { get; set; }
        public string? Server { get; set; }
    }
}