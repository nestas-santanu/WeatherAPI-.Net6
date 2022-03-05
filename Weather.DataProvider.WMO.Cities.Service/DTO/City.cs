namespace Weather.DataProvider.WMO.Cities.Service.DTO
{
    //public class CitiesByCountry
    //{
    //    public string Country { get; set; } = null!;
    //    public List<City> Cities { get; set; } = null!;
    //}

    //public class City
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; } = null!;
    //}

    public class City
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Country { get; set; } = null!;
    }
}