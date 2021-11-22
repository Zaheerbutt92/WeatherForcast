using System;
using System.Collections.Generic;

namespace back_end.ViewModels
{
    public class OpenWeatherResponse
    {
        public int Code { get; set; }
        public string Message { get; set; }
        public List<OpenWeatherForecast> List { get; set; }
        public City City { get; set; }
    }

    public class OpenWeatherForecast{
        public Main main { get; set; }
        public List<Weather> weather { get; set; }
        public Wind wind { get; set; }
        public DateTime dateTime { get; set; }
    }
    public class Main
    {
        public float Temp { get; set; }
        public float TempMin { get; set; }
        public float TempMax { get; set; }
        public float Humidity { get; set; }
    }
    public class Weather
    {
        public string Main { get; set; }
        public string Description { get; set; }
    }
    public class Wind
    {
        public float Speed { get; set; }
    }
    public class City {
        public string Name { get; set; }
        public string Country { get; set; }
    }
}