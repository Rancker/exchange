using ExchangeApi.Model;

namespace ExchangeApi.Service.Interface
{
    public interface IExchangeRateService
    {
        Task<ExchangeRate> GetLatestExchangeRateAsync(string currencyCode, decimal amount, CancellationToken cancellationToken);
    }
}
