using FxCalculator.Core.Entities;
using FxCalculator.Core.Shared;

namespace FxCalculator.Core.Interfaces;

public interface ICurrencyCalculator
{
    public Result<Money> Calculate(Money from, string toCurrency);
}