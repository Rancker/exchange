namespace ExchangeApi.Service.Interface;

public interface ICurrencyConversionProcessor
{
    Task<decimal> Covert(string currencyCode, decimal amount, CancellationToken cancellationToken);
}