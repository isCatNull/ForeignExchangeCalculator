using FxCalculator.Application;
using FxCalculator.Core.Entities;
using FxCalculator.Infrastructure;
using NUnit.Framework;

namespace FxCalculator.Tests.FxCalculator.CurrencyCalculator;

[TestFixture]
public class CurrencyCalculatorTests
{
    private CurrencyCalculatorService _sut;
    private static object[] _testCases =
    [
        new object[] { "DKK", "DKK", 45m, 45m },
        new object[] { "EUR", "EUR", 1.56m, 1.56m },
        new object[] { "USD", "USD", 167m, 167m },
        new object[] { "DKK", "EUR", 743.94m, 100m },
        new object[] { "DKK", "USD", 663.11m, 100m },
        new object[] { "DKK", "GBP", 852.85m, 100m },
        new object[] { "DKK", "SEK", 76.10m, 100m },
        new object[] { "DKK", "NOK", 78.40m, 100m },
        new object[] { "DKK", "CHF", 683.58m, 100m },
        new object[] { "DKK", "JPY", 5.9740m, 100m },
        new object[] { "EUR", "DKK", 1m, 7.4394m },
        new object[] { "EUR", "GBP", 1m, 0.8723m },
        new object[] { "EUR", "USD", 1m, 1.1219m },
        new object[] { "EUR", "USD", 0m, 0m }
    ];

    [SetUp]
    public void Setup()
    {
        _sut = new CurrencyCalculatorService(new StaticCurrencyRateProvider());
    }
    
    [TestCaseSource(nameof(_testCases))]
    public void Given_CurrenciesToExchange_ReturnsCorrectAmount(string fromCurrency, string toCurrency,
        decimal amountToExchange, decimal expectedExchangedAmount)
    {
        // Arrange & Act
        var result = _sut.ExchangeCurrency(new Money(amountToExchange, fromCurrency), toCurrency);

        // Assert
        Assert.That(result.IsSuccess, Is.True);
        Assert.That(result.Value.Amount, Is.EqualTo(expectedExchangedAmount));
        Assert.That(result.Value.Currency, Is.EqualTo(toCurrency));
    }
}