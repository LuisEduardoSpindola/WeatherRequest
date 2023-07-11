using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WeatherRequest.Models;
using WeatherRequest.Services;

namespace WeatherRequest.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Response(WeatherRequestModel model)
        {
            // Lógica para obter as coordenadas
            GetWeatherState getWeatherState = new GetWeatherState();
            WeatherRequestModel result = await getWeatherState.GetCoordinatesByCityName(model);

            return View(result);
        }
    }
}