using Newtonsoft.Json.Converters;
using Newtonsoft.Json;
using Weather.Web.Models;

namespace Weather.Web.Services
{
    public class WeatherForecastService
    {
        readonly HttpClient _httpClient;
        readonly IConfiguration _configuration;
        readonly string _apiUrl;

        public WeatherForecastService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;

            _apiUrl = _configuration["WeatherApp:APIUrl"];
        }

        public async Task<IEnumerable<CityInfo>> GetAllCities()
        {
            var cityInfoUrl = _configuration["WeatherApp:CityInfoUrl"];

            HttpRequestMessage msg = new(HttpMethod.Get,
                $"{_apiUrl}{cityInfoUrl}");

            var response = await _httpClient.SendAsync(msg);

            var responseContent = await response.Content.ReadAsStringAsync();

            if (string.IsNullOrEmpty(responseContent))
                return new List<CityInfo>();

            return JsonConvert.DeserializeObject<List<CityInfo>>(responseContent);
        }

        public async Task<WeatherForecast?> GetWeatherForCity(string cityName)
        {
            var weatherForCityUrl = _configuration["WeatherApp:WeatherForCityUrl"];

            HttpRequestMessage msg = new(HttpMethod.Get, 
                $"{_apiUrl}{weatherForCityUrl}?cityName={cityName}");

            var response = await _httpClient.SendAsync(msg);

            var responseContent = await response.Content.ReadAsStringAsync();

            if (string.IsNullOrEmpty(responseContent))
                return default;

            var forecast = JsonConvert.DeserializeObject<WeatherForecast>(responseContent);

            return forecast;
        }
    
        public async Task MarkWeatherAsIncorrect(string cityName)
        {
            var reportErrorUrl = _configuration["WeatherApp:ReportErrorUrl"];

            HttpRequestMessage msg = new HttpRequestMessage(HttpMethod.Post,
                $"{_apiUrl}{reportErrorUrl}?value={cityName}");

            var response = await _httpClient.SendAsync(msg);
            response.EnsureSuccessStatusCode();
        }
    }
}
