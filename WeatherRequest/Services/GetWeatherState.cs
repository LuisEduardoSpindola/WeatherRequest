using System.Text.Json;
using WeatherRequest.Models;

// Test: https://api.openweathermap.org/data/2.5/weather?lat=51.5156177&lon=-0.0919983&lang=pt-br&appid=6006483820e66b91c515696f4559a2bf

namespace WeatherRequest.Services
{
    public class GetWeatherState
    {
        private string apiKey = "6006483820e66b91c515696f4559a2bf";
        public string lang = "pt-br";

        public async Task<WeatherRequestModel> GetWeatherByCoordinates(WeatherRequestModel model)
        {
            string weatherUrl = $"https://api.openweathermap.org/data/2.5/weather?lat={model.Latitude}&lon={model.Longitude}&appid={apiKey}&lang=pt_br&units=metric";

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(weatherUrl);
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                    var result = JsonSerializer.Deserialize<WeatherResponse>(json, options);


                    string country = result.sys.country;
                    float temp = result.main.temp;
                    float feelsLike = result.main.feels_like;
                    string main = result.weather[0].main;
                    string description = result.weather[0].description;
                    string icon = result.weather[0].icon;
                    int humidity = result.main.humidity;

                    model.Country = country;
                    model.Temp = (int)temp;
                    model.FeelsLike = (int)feelsLike;
                    model.Main = main;
                    model.Description = description;
                    model.Icon = icon;
                    model.Humidity = humidity;

                    return model;
                }
            }

            return null; // Em caso de falha na requisição ou erro de processamento
        }
    }
}
