namespace Weather.DataProvider.WMO.Cities.Service.Data
{
    public interface IRead
    {
        Task<List<DTO.City>> GetCitiesAsync();
    }
}