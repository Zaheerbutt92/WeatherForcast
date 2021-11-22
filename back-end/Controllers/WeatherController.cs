using System.Threading.Tasks;
using back_end.Helpers;
using back_end.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace back_end.Controllers
{
    public class WeatherController : BaseApiController
    {
        private readonly IOpenWeatherService _openWeatherService;
        public WeatherController(IOpenWeatherService openWeatherService)
        {
            _openWeatherService = openWeatherService;
        }

        [HttpGet("forecast")]
        public async Task<ActionResult> GetForecast([FromQuery] Params parms)
        {
            return Ok(await _openWeatherService.GetForecast(parms));
        }
    }
}