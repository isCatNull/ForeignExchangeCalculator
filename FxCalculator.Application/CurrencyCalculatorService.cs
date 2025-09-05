using FxCalculator.Core.Entities;
using FxCalculator.Core.Interfaces;
using FxCalculator.Core.Shared;

namespace FxCalculator.Application;

public class CurrencyCalculatorService(ICurrencyRateProvider currencyRateProvider) : ICurrencyCalculatorService
{
    public Result<Money> ExchangeCurrency(Money from, string toCurrency)
    {
        var exchangeRate = currencyRateProvider.GetRate(from.Currency, toCurrency);
        
        if(exchangeRate == null)
            return Result<Money>.Failure(ErrorMessages.ExchangeRateNotFound(from.Currency, toCurrency));

        var converted = from.Amount * exchangeRate.Value;
        
        return Result<Money>
            .Success(new Money(Math.Round(converted, 4, MidpointRounding.AwayFromZero), toCurrency));
    }
}