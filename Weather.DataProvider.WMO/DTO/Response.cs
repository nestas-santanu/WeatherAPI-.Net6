namespace Weather.DataProvider.WMO.DTO
{
    public class Response
    {
        public int StatusCode { get; set; }
        public string? Message { get; set; }

        //public ForecastData? Data { get; set; }
        public Service.DTO.ForecastValues? Forecast { get; set; }
    }
}