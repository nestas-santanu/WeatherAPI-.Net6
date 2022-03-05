using LazyCache;
using log4net;
using Weather.DataProvider.WMO.Cities.Service.DTO;

namespace Weather.DataProvider.WMO.Cities.Data.FromFile
{
    public class Read : Weather.DataProvider.WMO.Cities.Service.Data.IRead
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(Read));

        private readonly IDataSource dataSource;
        private readonly IAppCache cache;

        public Read(IDataSource dataSource, IAppCache cache)
        {
            this.dataSource = dataSource;
            this.cache = cache;
        }

        public async Task<List<City>> GetCitiesAsync()
        {
            try
            {
                //return await ReadDataFromFileAsync().ConfigureAwait(false);

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