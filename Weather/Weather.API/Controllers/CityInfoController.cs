using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Weather.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CityInfoController : ControllerBase
    {
        readonly CityDataService _cityDataService;

        public CityInfoController(CityDataService cityDataService)
        {
            _cityDataService = cityDataService;
        }

        // GET: api/<CityInfoController>
        [HttpGet]
        [Produces("application/json")]
        public async Task<ActionResult<IEnumerable<CityInfo>>> Get()
        {
            IEnumerable<CityInfo> allCities = await _cityDataService.GetAllCities();

            return new OkObjectResult(allCities);
        }
    }
}
