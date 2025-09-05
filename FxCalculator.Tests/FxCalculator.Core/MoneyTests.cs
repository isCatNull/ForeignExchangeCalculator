using FxCalculator.Core.Entities;
using FxCalculator.Core.Shared;
using NUnit.Framework;

namespace FxCalculator.Tests.FxCalculator.Core;

[TestFixture]
public class MoneyTests
{
    private const string DefaultCurrencyForTest = "DKK";
    private const decimal DefaultAmountForTest = 100.00M;
    
    [TestCase("EUR", 10.00)]
    [TestCase("GBP", 45.00)]
    [TestCase("DKK", 10000)]
    [TestCase("SEK", 0.00)]
    public void Given_ValidCurrencyAmountAndCode_SuccessfullyCreatesMoney(string currency, decimal amount)
    {
        // Arrange & Act
        var result = Money.Create(currency, amount);

        // Assert
        Assert.That(result.IsSuccess, Is.True);
        Assert.That(result.Value.Amount, Is.EqualTo(amount));
        Assert.That(result.Value.Currency, Is.EqualTo(currency));
    }
    
    [TestCase("EUUUU")]
    [TestCase("G")]
    [TestCase("GG")]
    public void Given_InvalidCurrency_FailsToCreateMoney(string currency)
    {
        // Arrange & Act
        var result = Money.Create(currency, DefaultAmountForTest);

        // Assert
        Assert.That(result.IsSuccess, Is.False);
        Assert.That(result.Error, Is.EqualTo(ErrorMessages.InvalidCurrencyCode));
    }
    
    [TestCase(-1)]
    [TestCase(-45.00)]
    public void Given_NegativeAmount_FailsToCreateMoney(decimal amount)
    {
        // Arrange & Act
        var result = Money.Create(DefaultCurrencyForTest, amount);

        // Assert
        Assert.That(result.IsSuccess, Is.False);
        Assert.That(result.Error, Is.EqualTo(ErrorMessages.NegativeAmount));
    }
    
    [TestCase(null)]
    [TestCase("")]
    [TestCase("  ")]
    public void Given_EmptyCurrency_FailsToCreateMoney(string currency)
    {
        // Arrange & Act
        var result = Money.Create(currency, DefaultAmountForTest);

        // Assert
        Assert.That(result.IsSuccess, Is.False);
        Assert.That(result.Error, Is.EqualTo(ErrorMessages.EmptyCurrency));
    }
}