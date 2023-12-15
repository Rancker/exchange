using ExchangeApi.Model;
using ExchangeApi.Service;
using ExchangeApi.Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace ExchangeApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class PricingController : ControllerBase
    {
        public ICurrencyConversionProcessor _currencyConversionProcessor { get; }

        public PricingController(ICurrencyConversionProcessor currencyConversionProcessor)
        {
            _currencyConversionProcessor = currencyConversionProcessor;
        }

        [HttpGet("convert")]
        public async Task<ActionResult<decimal>> GetExchangeRateAsync([FromQuery] string currencyCode, [FromQuery] decimal amount, CancellationToken cancellationToken)
        {
            var response = await _currencyConversionProcessor.Covert(currencyCode, amount, cancellationToken);
            return Ok(response);
        }
    }
}

