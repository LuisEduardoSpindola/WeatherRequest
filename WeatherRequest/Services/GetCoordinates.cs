﻿using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using WeatherRequest.Models;

namespace WeatherRequest.Services
{
    public class GetCoordinates
    {
        private string apiKey = "6006483820e66b91c515696f4559a2bf";

        public class Coordinates
        {
            public double lat { get; set; }
            public double lon { get; set; }
        }

        public async Task<WeatherRequestModel> GetCoordinatesByCityName(WeatherRequestModel model)
        {
            string cityUrl = $"http://api.openweathermap.org/geo/1.0/direct?q={model.CityName}&appid={apiKey}";

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(cityUrl);
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                    var result = JsonSerializer.Deserialize<Coordinates[]>(json, options);
                    double Latitude = result[0].lat;
                    double Longitude = result[0].lon;

                    // Armazenar as coordenadas no modelo
                    model.Latitude = Latitude;
                    model.Longitude = Longitude;

                    return model;
                }
            }

            return null; // Em caso de falha na requisição ou erro de processamento
        }
    }
}
