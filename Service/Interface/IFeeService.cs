namespace ExchangeApi.Service.Interface;

public interface IFeeService
{
    Task<decimal> GetFeeAsync(decimal amount);
}