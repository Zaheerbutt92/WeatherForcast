using AutoMapper;
using back_end.ViewModels;
using Newtonsoft.Json.Linq;

namespace back_end.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {

            CreateMap<JToken, OpenWeatherForecast>()
                .ForMember(dest => dest.dateTime, cfg => { cfg.MapFrom(src => (string)src["dt_txt"]); })
                .ForMember(dest => dest.weather, cfg => { cfg.MapFrom(src => src["weather"]); })
                .ForMember(dest => dest.wind, cfg => { cfg.MapFrom(src => src["wind"]); })
                .ForMember(dest => dest.main, cfg => { cfg.MapFrom(src => src["main"]); });


            CreateMap<JToken, Weather>()
                .ForMember(dest => dest.Main, cfg => { cfg.MapFrom(src => (string)src["main"]); })
                .ForMember(dest => dest.Description, cfg => { cfg.MapFrom(src => (string)src["description"]); });

            CreateMap<JToken, Main>()
                .ForMember(dest => dest.Temp, cfg => { cfg.MapFrom(src => src["temp"]); })
                .ForMember(dest => dest.TempMax, cfg => { cfg.MapFrom(src => src["temp_max"]); })
                .ForMember(dest => dest.TempMin, cfg => { cfg.MapFrom(src => src["temp_min"]); })
                .ForMember(dest => dest.Humidity, cfg => { cfg.MapFrom(src => src["humidity"]); });

            CreateMap<JToken, Wind>()
                .ForMember(dest => dest.Speed, cfg => { cfg.MapFrom(src => src["speed"]); });

            CreateMap<JToken, City>()
                .ForMember(dest => dest.Name, cfg => { cfg.MapFrom(src => (string)src["name"]); })
                .ForMember(dest => dest.Country, cfg => { cfg.MapFrom(src => (string)src["country"]); });

            CreateMap<JToken, OpenWeatherResponse>()
                .ForMember(dest => dest.Code, cfg => { cfg.MapFrom(src => src["cod"]); })
                .ForMember(dest => dest.Message, cfg => { cfg.MapFrom(src => src["message"]); })
                .ForMember(dest => dest.List, cfg => { cfg.MapFrom(src => src["list"]); })
                .ForMember(dest => dest.City, cfg => { cfg.MapFrom(src => src["city"]); });
        }
    }
}