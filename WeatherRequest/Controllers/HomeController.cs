using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Threading.Tasks;
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
            GetCoordinates getCoordinates = new GetCoordinates();
            WeatherRequestModel coordinatesResult = await getCoordinates.GetCoordinatesByCityName(model);

            // Verifica se as coordenadas foram obtidas corretamente
            if (coordinatesResult != null)
            {
                // Lógica para obter as informações climáticas
                GetWeatherState getWeatherState = new GetWeatherState();
                WeatherRequestModel weatherResult = await getWeatherState.GetWeatherByCoordinates(coordinatesResult);

                if (weatherResult != null)
                {
                    return View(weatherResult);
                }
            }

            return View("Error");
        }
    }
}
