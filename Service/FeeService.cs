using System.Runtime.InteropServices.ComTypes;
using ExchangeApi.Configuration;
using ExchangeApi.Service.Interface;
using Microsoft.FeatureManagement;

namespace ExchangeApi.Service;

public class FeeService : IFeeService
{
    private readonly Dictionary<(decimal start, decimal end), decimal> feesRange = new Dictionary<(decimal start, decimal end), decimal>
    {
        {(0, 9.99m), 50},
        {(10, 99.99m), 5},
        {(100, decimal.MaxValue), 5},

    };

    public async Task<decimal> GetFeeAsync(decimal amount)
    {
       if (amount <= 0) return 0;
       var feePercentage = feesRange.Where(x => amount >= x.Key.start && amount <= x.Key.end).Select(x => x.Value)
           .FirstOrDefault();
       return (amount * feePercentage) / 100;
    }
}