using FxCalculator.Core.Entities;
using FxCalculator.Core.Shared;

namespace FxCalculator.Core.Interfaces;

public interface ICurrencyCalculatorService
{
    public Result<Money> ExchangeCurrency(Money from, string toCurrency);
}