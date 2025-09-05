using FxCalculator.Core.Interfaces;

namespace FxCalculator.Infrastructure;

public class StaticCurrencyRateProvider : ICurrencyRateProvider
{
    private const decimal BasePerDkk = 100m;

    private readonly Dictionary<string, decimal> _rates = new()
    {
        { "DKK", 100m },
        { "EUR", 743.94m },
        { "USD", 663.11m },
        { "GBP", 852.85m },
        { "SEK", 76.10m },
        { "NOK", 78.40m },
        { "CHF", 683.58m },
        { "JPY", 5.9740m }
    };
    
    public decimal? GetRate(string fromCurrency, string toCurrency)
    {
        if(!_rates.TryGetValue(fromCurrency, out var fromRate) ||
           !_rates.TryGetValue(toCurrency, out var toRate))
        {
            return null;
        }
        
        if (fromCurrency == toCurrency) return 1m;

        var rate = (fromRate / BasePerDkk) / (toRate / BasePerDkk);

        return rate;
    }
}