using FxCalculator.Core.Entities;
using FxCalculator.Core.Interfaces;
using FxCalculator.Core.Shared;

namespace FxCalculator.UseCases;

public class CurrencyCalculatorService(ICurrencyRateProvider currencyRateProvider) : ICurrencyCalculatorService
{
    public Result<Money> Calculate(Money from, string toCurrency)
    {
        if (from.Currency.Equals(toCurrency, StringComparison.OrdinalIgnoreCase))
            return Result<Money>.Success(from);
        
        var exchangeRate = currencyRateProvider.GetRate(from.Currency, toCurrency);
        
        if(exchangeRate == null)
            return Result<Money>.Failure(ErrorMessages.ExchangeRateNotFound(from.Currency, toCurrency));

        var calculatedMoney = from.Amount * exchangeRate.Value;
        
        return Result<Money>.Success(new Money(calculatedMoney, toCurrency));
    }
}