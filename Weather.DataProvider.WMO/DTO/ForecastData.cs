using System.Text.Json.Serialization;

namespace Weather.DataProvider.WMO.DTO
{
    public class ForecastData
    {
        [JsonPropertyName("city")]
        public City? City { get; set; }
    }

    public class City
    {
        [JsonPropertyName("lang")]
        public string Lang { get; set; } = String.Empty;

        [JsonPropertyName("cityName")]
        public string CityName { get; set; } = String.Empty;

        [JsonPropertyName("cityLatitude")]
        public string CityLatitude { get; set; } = String.Empty;

        [JsonPropertyName("cityLongitude")]
        public string CityLongitude { get; set; } = String.Empty;

        [JsonPropertyName("cityId")]
        public int CityId { get; set; }

        [JsonPropertyName("isCapital")]
        public bool IsCapital { get; set; }

        [JsonPropertyName("stationName")]
        public string StationName { get; set; } = String.Empty;

        [JsonPropertyName("tourismURL")]
        public string TourismURL { get; set; } = String.Empty;

        [JsonPropertyName("tourismBoardName")]
        public string TourismBoardName { get; set; } = String.Empty;

        [JsonPropertyName("isDep")]
        public bool IsDep { get; set; }

        [JsonPropertyName("timeZone")]
        public string TimeZone { get; set; } = String.Empty;

        [JsonPropertyName("isDST")]
        public string IsDST { get; set; } = String.Empty;

        [JsonPropertyName("member")]
        public Member Member { get; set; } = null!;

        [JsonPropertyName("forecast")]
        public CityForecast? Forecast { get; set; }

        [JsonPropertyName("climate")]
        public Climate? Climate { get; set; }
    }

    public class Member
    {
        [JsonPropertyName("memId")]
        public int MemId { get; set; }

        [JsonPropertyName("memName")]
        public string MemName { get; set; } = String.Empty;

        [JsonPropertyName("shortMemName")]
        public string ShortMemName { get; set; } = String.Empty;

        [JsonPropertyName("url")]
        public string Url { get; set; } = String.Empty;

        [JsonPropertyName("orgName")]
        public string OrgName { get; set; } = String.Empty;

        [JsonPropertyName("logo")]
        public string Logo { get; set; } = String.Empty;

        [JsonPropertyName("ra")]
        public int RegionId { get; set; }
    }

    public class CityForecast
    {
        [JsonPropertyName("issueDate")]
        public string IssueDate { get; set; } = String.Empty;

        [JsonPropertyName("timeZone")]
        public string TimeZone { get; set; } = String.Empty;

        [JsonPropertyName("forecastDay")]
        public List<ForecastDay> ForecastDay { get; set; } = new();
    }

    public class ForecastDay
    {
        [JsonPropertyName("forecastDate")]
        public string ForecastDate { get; set; } = String.Empty;

        [JsonPropertyName("wxdesc")]
        public string Wxdesc { get; set; } = String.Empty;

        [JsonPropertyName("weather")]
        public string Weather { get; set; } = String.Empty;

        [JsonPropertyName("minTemp")]
        public string MinTemp { get; set; } = String.Empty;

        [JsonPropertyName("maxTemp")]
        public string MaxTemp { get; set; } = String.Empty;

        [JsonPropertyName("minTempF")]
        public string MinTempF { get; set; } = String.Empty;

        [JsonPropertyName("maxTempF")]
        public string MaxTempF { get; set; } = String.Empty;

        [JsonPropertyName("weatherIcon")]
        public int WeatherIcon { get; set; }
    }

    public class Climate
    {
        [JsonPropertyName("raintype")]
        public string Raintype { get; set; } = String.Empty;

        [JsonPropertyName("raindef")]
        public string Raindef { get; set; } = String.Empty;

        [JsonPropertyName("rainunit")]
        public string Rainunit { get; set; } = String.Empty;

        [JsonPropertyName("datab")]
        public int Datab { get; set; }

        [JsonPropertyName("datae")]
        public int Datae { get; set; }

        [JsonPropertyName("tempb")]
        public string Tempb { get; set; } = String.Empty;

        [JsonPropertyName("tempe")]
        public string Tempe { get; set; } = String.Empty;

        [JsonPropertyName("rdayb")]
        public string Rdayb { get; set; } = String.Empty;

        [JsonPropertyName("rdaye")]
        public string Rdaye { get; set; } = String.Empty;

        [JsonPropertyName("rainfallb")]
        public string Rainfallb { get; set; } = String.Empty;

        [JsonPropertyName("rainfalle")]
        public string Rainfalle { get; set; } = String.Empty;

        [JsonPropertyName("climatefromclino")]
        public string Climatefromclino { get; set; } = String.Empty;

        [JsonPropertyName("climateMonth")]
        public List<ClimateMonth> ClimateMonth { get; set; } = null!;
    }

    public class ClimateMonth
    {
        [JsonPropertyName("month")]
        public int Month { get; set; }

        [JsonPropertyName("maxTemp")]
        public string MaxTemp { get; set; } = String.Empty;

        [JsonPropertyName("minTemp")]
        public string MinTemp { get; set; } = String.Empty;

        [JsonPropertyName("meanTemp")]
        public object MeanTemp { get; set; } = String.Empty;

        [JsonPropertyName("maxTempF")]
        public string MaxTempF { get; set; } = String.Empty;

        [JsonPropertyName("minTempF")]
        public string MinTempF { get; set; } = String.Empty;

        [JsonPropertyName("meanTempF")]
        public object MeanTempF { get; set; } = String.Empty;

        [JsonPropertyName("raindays")]
        public object Raindays { get; set; } = String.Empty;

        [JsonPropertyName("rainfall")]
        public string Rainfall { get; set; } = String.Empty;

        [JsonPropertyName("climateFromMemDate")]
        public string ClimateFromMemDate { get; set; } = String.Empty;
    }
}