using System;
using System.Collections.Generic;


namespace back_end.ViewModels
{
    public class CustomForecastResponse
    {
        public string City { get; set; }
        public string Country { get; set; }
        public IEnumerable<WeatherForecast> List { get; set; }
    }
    public class WeatherForecast
    {
        public DateTime Date { get; set; }
        public float AverageTemp { get; set; }
        public float MinTemp { get; set; }
        public float MaxTemp { get; set; }
        public float AverageHumidity { get; set; }
        public float MinHumidity { get; set; }
        public float MaxHumidity { get; set; }
        public float WindSpeed { get; set; }
        public string MostlyWeather { get; set; }
    }
}