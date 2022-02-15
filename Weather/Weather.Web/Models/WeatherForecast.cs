namespace Weather.Web.Models
{
    public class WeatherForecast
    {

        public DateTime Date { get; set; }

        public int TemperatureC { get; set; }

        public int TemperatureF { get; set; }

        public string? CityName { get; set; }

        public string? Summary { get; set; }
    }
}
