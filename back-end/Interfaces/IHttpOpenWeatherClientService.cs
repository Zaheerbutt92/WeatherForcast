using System.Threading.Tasks;

namespace back_end.Interfaces
{
    public interface IHttpOpenWeatherClientService
    {
        Task<T> GetAsync<T>(string url);
    }
}