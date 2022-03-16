using LazyCache;
using log4net;
using Weather.DataProvider.WMO.Data.Cities.DTO;
using Weather.DataProvider.WMO.Resource.Cities.Data.File;

namespace Weather.DataProvider.WMO.Data.File.Cities
{
    public class Cities : Weather.DataProvider.WMO.Data.Cities.ICity
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(Cities));

        private readonly Weather.DataProvider.WMO.Resource.Cities.Data.File.IDataSource dataSource;
        private readonly IAppCache cache;

        public Cities(IDataSource dataSource, IAppCache cache)
        {
            this.dataSource = dataSource;
            this.cache = cache;
        }

        public async Task<List<string>> GetCountriesAsync()
        {
            List<string> countries = new();

            try
            {
                var data
                    = await GetCitiesAsync().ConfigureAwait(false);

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
                    = await GetCitiesAsync().ConfigureAwait(false);

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
                    = await GetCitiesAsync().ConfigureAwait(false);

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
                    = await GetCitiesAsync().ConfigureAwait(false);

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
                    = await GetCitiesAsync().ConfigureAwait(false);

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

        private async Task<List<City>> GetCitiesAsync()
        {
            try
            {
                List<City> cities
                    = await cache
                        .GetOrAddAsync(
                            "Cities",
                            async () => await ReadDataFromFileAsync().ConfigureAwait(false),
                            new TimeSpan(24, 0, 0))
                        .ConfigureAwait(false);

                return cities;
            }
            catch (Exception e)
            {
                log.Error("", e);

                throw;
            }
        }

        private async Task<List<City>> ReadDataFromFileAsync()
        {
            List<City> cities = new();

            //get the file
            string? filePath = dataSource.FilePath;

            if (filePath == null)
            {
                throw new Exception("The path to the cities resource file could not be determined.");
            }

            //read the file
            //create the city object
            //add to cities list
            List<string> lines = new();

            using (StreamReader reader = new(filePath))
            {
                string? line = await reader.ReadLineAsync().ConfigureAwait(false);

                while (line != null)
                {
                    lines.Add(line);

                    line = await reader.ReadLineAsync().ConfigureAwait(false);
                }
            }

            if (lines.Count > 0)
            {
                //removes the header in the file
                lines.RemoveAt(0);
            }

            foreach (var line in lines)
            {
                try
                {
                    string[] split = line.Split(';');

                    cities.Add(new City
                    {
                        Id = Convert.ToInt32(split[2].Trim('"')),
                        Name = split[1].Trim('"'),
                        Country = split[0].Trim('"')
                    });
                }
                catch (Exception e)
                {
                    log.Error($"line: {line}", e);

                    continue;
                }
            }

            return cities;
        }
    }
}