using System.Globalization;
using ExchangeApi.Configuration;
using ExchangeApi.Service.Interface;
using Microsoft.FeatureManagement;

namespace ExchangeApi.Service
{
    public class CurrencyConversionProcessor : ICurrencyConversionProcessor
    {
        private readonly IExchangeRateService _exchangeRateService;
        private readonly IFeatureManager _featureManager;
        private readonly IFeeService _feeService;

        public CurrencyConversionProcessor(IExchangeRateService exchangeRateService, IFeatureManager featureManager, IFeeService feeService)
        {
            _exchangeRateService = exchangeRateService;
            _featureManager = featureManager;
            _feeService = feeService;
        }
        public async Task<decimal> Covert(string currencyCode, decimal amount, CancellationToken cancellationToken)
        {
            decimal fees = 0;
            if (await _featureManager.IsEnabledAsync(FeatureFlags.Rate))
            {
                fees = await GetFeesAsync(amount);
            }
            var exchangeRate = await _exchangeRateService.GetLatestExchangeRateAsync(currencyCode, amount, cancellationToken);

            if (string.Equals(nameof(exchangeRate.Rates.EUR),currencyCode, StringComparison.InvariantCultureIgnoreCase))
            {
                return amount * exchangeRate.Rates.EUR + fees;
            }

            if (string.Equals(nameof(exchangeRate.Rates.GBP),currencyCode, StringComparison.InvariantCultureIgnoreCase))
            {
                return amount * exchangeRate.Rates.GBP + fees;
            }

            if (string.Equals(nameof(exchangeRate.Rates.USD), currencyCode, StringComparison.InvariantCultureIgnoreCase))
            {
                return amount * exchangeRate.Rates.USD + fees;
            }

            return -1;
        }

        private async Task<decimal> GetFeesAsync(decimal amount)
        {
            return await _feeService.GetFeeAsync(amount);
        }
    }
}


