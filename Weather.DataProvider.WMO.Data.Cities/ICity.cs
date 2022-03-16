namespace Weather.DataProvider.WMO.Data.Cities
{
    public interface ICity
    {
        Task<List<string>> GetCountriesAsync();

        Task<List<DTO.City>> GetCitiesAsync(string countryName);

        Task<DTO.City?> GetCityAsync(string country, string city);

        Task<DTO.City?> GetCityAsync(int cityId);

        Task<List<DTO.City>> SearchCityAsync(string keyword);
    }
}