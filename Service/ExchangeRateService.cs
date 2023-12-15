using System.Text.Json;
using ExchangeApi.Configuration;
using ExchangeApi.Model;
using ExchangeApi.Service.Interface;
using Microsoft.Extensions.Options;

namespace ExchangeApi.Service
{
    public class ExchangeRateService : IExchangeRateService
    {
        private readonly IOptions<ExchangeConfigurationModel> _configuration;
        private readonly HttpClient _httpClient;

        public ExchangeRateService(IOptions<ExchangeConfigurationModel> configuration, HttpClient httpClient)
        {
            _configuration = configuration;
            _httpClient = httpClient;
        }

            /// <summary>
            /// Gets the latest exchange rate asynchronous.
            /// </summary>
            /// <returns></returns>
       public async Task<ExchangeRate> GetLatestExchangeRateAsync(string currencyCode , decimal amount, CancellationToken cancellationToken)
        {
            var response = await _httpClient.GetAsync(_httpClient.BaseAddress).Result.Content.ReadAsStringAsync(cancellationToken);
           return JsonSerializer.Deserialize<ExchangeRate>(response);
        }
    }
}
