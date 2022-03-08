using Microsoft.ApplicationInsights;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Weather.Web.Models;
using Weather.Web.Services;

namespace Weather.Web.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly WeatherForecastService _forecastService;

        private readonly TelemetryClient _telemetryClient;

        [BindProperty]
        public List<WeatherForecast> Forecasts { get; set; }

        public IndexModel(ILogger<IndexModel> logger, WeatherForecastService weatherForecastService, TelemetryClient telemetryClient)
        {
            _logger = logger;
            _forecastService = weatherForecastService;
            _telemetryClient = telemetryClient;

            Forecasts = new List<WeatherForecast>();
        }

        public async Task<IActionResult> OnPost(string cityName)
        {
            _telemetryClient.TrackEvent("Bad Forecast", new Dictionary<string, string> { { "City Name", cityName } });

            await _forecastService.MarkWeatherAsIncorrect(cityName);

            return RedirectToAction("Get");
        }

        public async Task OnGetAsync()
        {
            
            Forecasts.Clear();

            List<CityInfo> allCities = new(await _forecastService.GetAllCities());

            foreach (var city in allCities)
            {
                Forecasts.Add(await _forecastService.GetWeatherForCity(city.Name));
            }
        }
    }
}