using System.Net.Http;
using System.Threading.Tasks;
using back_end.Interfaces;
using Newtonsoft.Json;

namespace back_end.Services
{
    public class HttpOpenWeatherClientService : IHttpOpenWeatherClientService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly string _clientName;
        private readonly string _apiKey;
        HttpClient _client;
        public HttpOpenWeatherClientService(IHttpClientFactory httpClientFactory, string clientName, string apiKey)
        {
            _httpClientFactory = httpClientFactory;
            _clientName = clientName;
            _apiKey = apiKey;
        }

        public async Task<T> GetAsync<T>(string param)
        {
            _client = _httpClientFactory.CreateClient(_clientName);
            string queryString = "/data/2.5/forecast?" + param + "&apikey=" + _apiKey + "&units=metric";
            using (HttpResponseMessage response = await _client.GetAsync(queryString))
            using (HttpContent content = response.Content)
            {
                string _content = await content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(_content);
            }
        }
    }
}