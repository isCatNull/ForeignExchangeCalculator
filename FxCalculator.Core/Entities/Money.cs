using FxCalculator.Core.Shared;

namespace FxCalculator.Core.Entities;

public readonly record struct Money(decimal Amount, string Currency)
{
    public static Result<Money> Create(string currency, decimal amount)
    {
        if(amount < 0)
            return Result<Money>.Failure(ErrorMessages.NegativeAmount);
        
        if(string.IsNullOrWhiteSpace(currency))
            return Result<Money>.Failure(ErrorMessages.EmptyCurrency);
        
        if(currency.Length != 3)
            return Result<Money>.Failure(ErrorMessages.InvalidCurrencyCode);
        
        return Result<Money>.Success(new Money(amount, currency));
    }
}