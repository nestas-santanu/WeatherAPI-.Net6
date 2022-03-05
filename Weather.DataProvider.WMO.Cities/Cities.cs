using log4net;
using Weather.DataProvider.WMO.Cities.Service.Data;
using Weather.DataProvider.WMO.Cities.Service.DTO;

namespace Weather.DataProvider.WMO.Cities
{
    public class Cities : Weather.DataProvider.WMO.Cities.Service.ICity
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(Cities));

        public readonly Weather.DataProvider.WMO.Cities.Service.Data.IRead dataReadService;

        public Cities(IRead dataReadService)
        {
            this.dataReadService = dataReadService;
        }

        public async Task<List<string>> GetCountriesAsync()
        {
            List<string> countries = new();

            try
            {
                var data
                    = await dataReadService.GetCitiesAsync().ConfigureAwait(false);

                if (data.Count == 0)
                {
                    return countries;
                }

                return data.Select(c => c.Country).Distinct<string>().ToList();
            }
            catch (Exception e)
            {
                log.Error("", e);

                throw;
            }
        }

        public async Task<List<City>> GetCitiesAsync(string countryName)
        {
            try
            {
                var data
                    = await dataReadService.GetCitiesAsync().ConfigureAwait(false);

                if (data.Count == 0)
                {
                    return new List<City>();
                }

                List<City> filteredData
                    = data
                        .Where(w => w.Country.Equals(countryName, StringComparison.OrdinalIgnoreCase))
                        .ToList();

                if (filteredData.Count == 0)
                {
                    return new List<City>();
                }

                return filteredData;
            }
            catch (Exception e)
            {
                log.Error($"CountryName: {countryName}", e);

                throw;
            }
        }

        public async Task<List<City>> SearchCityAsync(string keyword)
        {
            try
            {
                var data
                    = await dataReadService.GetCitiesAsync().ConfigureAwait(false);

                if (data.Count == 0)
                {
                    return new List<City>();
                }

                List<City> filteredData
                    = data
                        .Where(w => w.Name.Contains(keyword, StringComparison.InvariantCultureIgnoreCase))
                        .ToList();

                if (filteredData.Count == 0)
                {
                    return new List<City>();
                }

                return filteredData;
            }
            catch (Exception e)
            {
                log.Error($"Keyword: {keyword}", e);

                throw;
            }
        }

        public async Task<City?> GetCityAsync(string country, string city)
        {
            try
            {
                var data
                    = await dataReadService.GetCitiesAsync().ConfigureAwait(false);

                if (data.Count == 0)
                {
                    return null;
                }

                List<City> filteredData
                    = data
                        .Where(w => w.Country.Equals(country, StringComparison.OrdinalIgnoreCase)
                            && w.Name.Contains(city, StringComparison.InvariantCultureIgnoreCase))
                        .ToList();

                if (filteredData.Count == 0)
                {
                    return null;
                }

                return filteredData[0];
            }
            catch (Exception e)
            {
                log.Error($"Country: {country}, City: {city}", e);

                throw;
            }
        }

        public async Task<City?> GetCityAsync(int cityId)
        {
            try
            {
                var data
                    = await dataReadService.GetCitiesAsync().ConfigureAwait(false);

                if (data.Count == 0)
                {
                    return null;
                }

                List<City> filteredData
                    = data
                        .Where(w => w.Id == cityId)
                        .ToList();

                if (filteredData.Count == 0)
                {
                    return null;
                }

                return filteredData[0];
            }
            catch (Exception e)
            {
                log.Error($"CityId: {cityId}", e);

                throw;
            }
        }

        //public async Task<List<CitiesByCountry>> GetAllCitiesAsync()
        //{
        //    try
        //    {
        //        List<CitiesByCountry> citiesByCountries = new();

        //        var cities
        //            = await dataReadService.GetCitiesAsync().ConfigureAwait(false);

        //        if (cities.Count == 0)
        //        {
        //            return citiesByCountries;
        //        }

        //        IEnumerable<IGrouping<string, DTO.City>>? groupByCountry
        //            = cities.GroupBy(g => g.Country);

        //        foreach (var group in groupByCountry)
        //        {
        //            citiesByCountries.Add(new CitiesByCountry
        //            {
        //                Country = group.Key,
        //                Cities = GetCities(group)
        //            });
        //        }

        //        return citiesByCountries;
        //    }
        //    catch (Exception e)
        //    {
        //        log.Error(e);

        //        throw;
        //    }
        //}

        //private static List<Service.DTO.City> GetCities(IGrouping<string, DTO.City> group)
        //{
        //    List<Service.DTO.City> cities = new(0);

        //    foreach (var item in group)
        //    {
        //        cities.Add(new City
        //        {
        //            Id = item.Id,
        //            Name = item.Name
        //        });
        //    }

        //    return cities;
        //}
    }
}