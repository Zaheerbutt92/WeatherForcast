using System;
using System.Net.Http;
using back_end.Helpers;
using back_end.Interfaces;
using back_end.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace back_end.Extensions
{
    public static class ApplicationExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
        {

            services.Configure<OpenWeatherSettings>(config.GetSection("OpenWeatherSettings"));
            services.AddScoped<IOpenWeatherService, OpenWeatherService>();
            services.AddAutoMapper(typeof(AutoMapperProfiles).Assembly);

            // HttpClientFactory
            services.AddHttpClient("OpenWeatherAPI", client =>
            {
                client.BaseAddress = new Uri(config.GetValue<string>("OpenWeatherSettings:BaseUrl"));
            });

            // HttpClientFactory will take care of connection caching, OpenWeatherAPI is the name 
            // of the factory, just above.
            services.AddSingleton<IHttpOpenWeatherClientService, HttpOpenWeatherClientService>(s =>
                         new HttpOpenWeatherClientService(
                             s.GetService<IHttpClientFactory>(),
                             "OpenWeatherAPI",
                             config.GetValue<string>("OpenWeatherSettings:ApiKey")
                            ));


            services.AddCors(opt =>
            {
                opt.AddPolicy("CorsPolicy", policy =>
                {
                    policy.AllowAnyMethod().AllowAnyHeader().WithOrigins("http://localhost:8080");
                });
            });
            return services;
        }
    }
}