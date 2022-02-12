using Microsoft.Azure.Cosmos;

namespace Weather.API
{
    public class CityDataService
    {
        readonly CosmosClient _client;
        readonly string _databaseName;
        readonly string _containerId;

        public CityDataService(string endpoint, string databaseName, string containerId)
        {
            _client = new CosmosClient(endpoint);
         
            _databaseName = databaseName;
            _containerId = containerId;
        }

        public async Task<IEnumerable<CityInfo>> GetAllCities()
        {
            List<CityInfo> cities = new();

            var cityContainer = _client.GetContainer(_databaseName, _containerId);

            var cityQueryResults = cityContainer.GetItemQueryIterator<CityInfo>();

            while (cityQueryResults.HasMoreResults)
            {
                cities.AddRange(await cityQueryResults.ReadNextAsync());
            }

            return cities;
        }

        ~CityDataService()
        {
            _client.Dispose();
        }
    }
}
