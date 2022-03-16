namespace Weather.DataProvider.WMO.Data.Cities.DTO
{
    public class City
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Country { get; set; } = null!;
    }
}