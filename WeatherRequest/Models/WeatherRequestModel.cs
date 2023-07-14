using System.ComponentModel.DataAnnotations;

namespace WeatherRequest.Models
{
    public class WeatherRequestModel
    {
        [Required]

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        //------------------------------------

        public string CityName { get; set; }

        public string Country { get; set; }

        public int Temp { get; set; }

        public int FeelsLike { get; set; }

        public string Main { get; set; }

        public string Description { get; set; }


        public string Icon { get; set; }

        public int Humidity { get; set; }
    }
}
