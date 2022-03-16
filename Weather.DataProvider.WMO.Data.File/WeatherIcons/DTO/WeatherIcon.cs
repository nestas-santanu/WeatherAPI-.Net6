using System.Text.Json.Serialization;

namespace Weather.DataProvider.WMO.Data.File.WeatherIcons.DTO
{
    internal class WeatherIcon
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; } = null!;

        [JsonPropertyName("icon")]
        public Icon Icon { get; set; } = null!;
    }

    internal class Icon
    {
        [JsonPropertyName("wmo")]
        public string WMO { get; set; } = null!;

        [JsonPropertyName("alt")]
        public string? Alt { get; set; }
    }
}