using FxCalculator.Core.Shared;

namespace FxCalculator.Core.Entities;

public readonly record struct Money(decimal Amount, string Currency)
{
    public static Result<Money> Create(string currency, decimal amount)
    {
        
        return Result<Money>.Success(new Money(amount, currency));
    }
}