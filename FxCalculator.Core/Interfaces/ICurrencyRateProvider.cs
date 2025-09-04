namespace FxCalculator.Core.Interfaces;

public interface ICurrencyRateProvider
{
    public decimal? GetRate(string fromCurrency, string toCurrency);
}