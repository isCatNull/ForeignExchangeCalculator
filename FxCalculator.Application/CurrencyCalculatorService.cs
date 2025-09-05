using FxCalculator.Core.Entities;
using FxCalculator.Core.Interfaces;
using FxCalculator.Core.Shared;

namespace FxCalculator.Application;

public class CurrencyCalculatorService(ICurrencyRateProvider currencyRateProvider) : ICurrencyCalculatorService
{
    private const int CurrencyDecimalPlaces = 4;
    
    public Result<Money> ExchangeCurrency(Money from, string toCurrency)
    {
        var exchangeRate = currencyRateProvider.GetRate(from.Currency, toCurrency);
        
        if(exchangeRate == null)
            return Result<Money>.Failure(ErrorMessages.ExchangeRateNotFound(from.Currency, toCurrency));

        var converted = from.Amount * exchangeRate.Value;
        
        return Result<Money>
            .Success(new Money(Math.Round(converted, CurrencyDecimalPlaces, MidpointRounding.AwayFromZero), toCurrency));
    }
}