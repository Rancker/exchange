using ExchangeApi.Configuration;
using ExchangeApi.Service;
using ExchangeApi.Service.Interface;
using Microsoft.Extensions.Configuration;
using Microsoft.FeatureManagement;
using Polly;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IExchangeRateService, ExchangeRateService>();
builder.Services.AddScoped<ICurrencyConversionProcessor, CurrencyConversionProcessor>();
builder.Services.AddScoped<IFeeService, FeeService>();
builder.Services.Configure<ExchangeConfigurationModel>(builder.Configuration.GetSection("ExchangeRateEndpoint"));
builder.Services.AddHttpClient<IExchangeRateService, ExchangeRateService>(
        client =>
        {
            client.BaseAddress = new Uri("https://trainlinerecruitment.github.io/exchangerates/api/latest/GBP.json");
            client.DefaultRequestHeaders.Add("Accept", "application/json");

        })
    .AddTransientHttpErrorPolicy(policy => policy.WaitAndRetryAsync(3, _ => TimeSpan.FromSeconds(2)))
    .AddTransientHttpErrorPolicy(policy => policy.CircuitBreakerAsync(5, TimeSpan.FromSeconds(5)));
builder.Services.AddFeatureManagement();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
