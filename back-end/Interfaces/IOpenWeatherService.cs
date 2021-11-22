using System.Threading.Tasks;
using back_end.Helpers;
using back_end.ViewModels;

namespace back_end.Interfaces
{
    public interface IOpenWeatherService
    {
        Task<Result<CustomForecastResponse>> GetForecast(Params parms);
    }
}