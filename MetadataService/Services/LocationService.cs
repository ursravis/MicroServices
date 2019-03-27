using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace MetadataService
{
    public class LocationService : ILocationService
    {
        private const string _locationIndexes = "locationIndexes";
        private readonly IDistributedCache _cache;
        private readonly ILogger<LocationService> _logger;

        public LocationService(IDistributedCache cache,ILogger<LocationService> logger)
        {
            _cache = cache;
            _logger = logger;
        }
        public async Task<double> GetInsuranceIndexByLocation(int zipcode)
        {
            try
            {
                var cacheData = await _cache.GetAsync<IEnumerable<InsuranceIndex>>(_locationIndexes);
                if (cacheData == null || cacheData.Count() == 0)
                {
                    cacheData = LoadLocatioIndexes();
                    await _cache.SetAsync<IEnumerable<InsuranceIndex>>(_locationIndexes, cacheData,
                                                                    new DistributedCacheEntryOptions() { AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(1) });
                }
                if (cacheData.FirstOrDefault(it => it.ZipCode == zipcode) != null)
                    return cacheData.FirstOrDefault(it => it.ZipCode == zipcode).Index;
            }
            catch (Exception ex)
            {
                    _logger.LogError(ex.Message);
            }

            return 1;
        }
        private IEnumerable<InsuranceIndex> LoadLocatioIndexes()
        {
            //Make external call to fetch data
            //Mocking data instead of callig actual service
            var indexes = new List<InsuranceIndex>();
            for (int i = 90000; i <= 99999; i++)
            {
                indexes.Add(new InsuranceIndex()
                {
                    ZipCode = i,
                    Index = GetRandomNumber(0, 1)
                });
            }
            return indexes;
        }
        private double GetRandomNumber(double minimum, double maximum)
        {
            Random random = new Random();
            return random.NextDouble() * (maximum - minimum) + minimum;
        }
    }
    public class InsuranceIndex
    {
        public int ZipCode { get; set; }
        public double Index { get; set; }

    }
}