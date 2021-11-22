using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using back_end.Helpers;
using back_end.Interfaces;
using back_end.ViewModels;
using Newtonsoft.Json.Linq;

namespace back_end.Services
{
    public class OpenWeatherService : IOpenWeatherService
    {
        private readonly IMapper _mapper;
        private readonly IHttpOpenWeatherClientService _http;
        public OpenWeatherService(IHttpOpenWeatherClientService http, IMapper mapper)
        {
            _http = http;
            _mapper = mapper;
        }

        async Task<Result<CustomForecastResponse>> IOpenWeatherService.GetForecast(Params parms)
        {
            if (parms is null || (parms.City is null && parms.ZipCode is null))
                return Result<CustomForecastResponse>.Failure("City not found");

            string _param = string.Empty;
            if (parms.City is not null)
                _param = "q=" + parms.City;
            if (parms.ZipCode is not null)
                _param = "zip=" + parms.ZipCode;

            var _openWeatherData = await _http.GetAsync<JToken>(_param);
            OpenWeatherResponse response = new();
            if (_openWeatherData is not null)
                response = _mapper.Map<OpenWeatherResponse>(_openWeatherData);

            if (response is not null && response.Code != 200)
                return Result<CustomForecastResponse>.Failure(response.Message);

            return Result<CustomForecastResponse>.Success(GetAverageForecastData(response));
        }

        #region Private Method
        private CustomForecastResponse GetAverageForecastData(OpenWeatherResponse src)
        {
            CustomForecastResponse response = new();
            response.City = src.City?.Name;
            response.Country = src.City?.Country;
            response.List = new List<WeatherForecast>();
            if (src.List.Any())
            {
                var eachDays =
                   from OpenWeatherResponse in src.List
                   group OpenWeatherResponse by
                    new
                    {
                        date = new DateTime(
                            OpenWeatherResponse.dateTime.Year,
                            OpenWeatherResponse.dateTime.Month,
                            OpenWeatherResponse.dateTime.Day)
                    }
                    into groupByDay
                   select new WeatherForecast
                   {
                       Date = groupByDay.Key.date,
                       AverageTemp = groupByDay.Average(x => x.main.Temp),
                       MinTemp = groupByDay.Min(x => x.main.TempMin),
                       MaxTemp = groupByDay.Max(x => x.main.TempMax),
                       AverageHumidity = groupByDay.Average(x => x.main.Humidity),
                       MinHumidity = groupByDay.Min(x => x.main.Humidity),
                       MaxHumidity = groupByDay.Max(x => x.main.Humidity),
                       WindSpeed = groupByDay.Min(x => x.wind.Speed),
                       MostlyWeather = findMostRepeativeMainWeather(groupByDay.Select(x => string.Join(",", x.weather.FirstOrDefault().Main)))
                   };
                response.List = eachDays;
            }
            return response;
        }

        private string findMostRepeativeMainWeather(IEnumerable<string> list)
        {
            var arr = list.ToArray();
            Dictionary<String, int> hs =
                new Dictionary<String, int>();

            for (int i = 0; i < arr.Count(); i++)
            {
                if (hs.ContainsKey(arr[i]))
                    hs[arr[i]] = hs[arr[i]] + 1;
                else
                    hs.Add(arr[i], 1);
            }

            String key = "";
            int value = 0;

            foreach (KeyValuePair<String, int> me in hs)
            {
                if (me.Value > value)
                {
                    value = me.Value;
                    key = me.Key;
                }
            }
            return key;
        }
        #endregion
    }
}