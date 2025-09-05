namespace FxCalculator.Core.Shared;

public static class ErrorMessages
{
    public const string NegativeAmount = "Amount cannot be negative.";
    public const string EmptyCurrency = "Currency cannot be empty.";
    public const string InvalidCurrencyCode = "Currency code must be a valid ISO three letter code.";
    public static string ExchangeRateNotFound(string from, string to) => $"Rate not found for {from} -> {to}";
}