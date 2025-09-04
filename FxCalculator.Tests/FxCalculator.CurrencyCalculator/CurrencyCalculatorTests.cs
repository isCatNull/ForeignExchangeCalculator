using FxCalculator.Core.Entities;
using FxCalculator.Core.Interfaces;
using FxCalculator.Core.Shared;
using FxCalculator.UseCases;
using NSubstitute;
using NUnit.Framework;

namespace FxCalculator.Tests.FxCalculator.CurrencyCalculator;

[TestFixture]
public class CurrencyCalculatorTests
{
    private ICurrencyRateProvider _mockCurrencyRateProvider;
    
    [SetUp]
    public void Setup()
    {
        _mockCurrencyRateProvider = Substitute.For<ICurrencyRateProvider>();
    }

    [TestCase(10, 1, 10)]
    [TestCase(0, 10, 0)]
    public void Given_ValidCurrencies_CalculatesCurrencyRate(decimal amount, decimal exchangeRate, decimal expectedResult)
    {
        // Arrange
        const string fromCurrency = "USD";
        const string toCurrency = "DKK";
        var currencyCalculator = new CurrencyCalculatorService(_mockCurrencyRateProvider);
        var fromMoney = Money.Create(fromCurrency, amount);
        
        _mockCurrencyRateProvider
            .GetRate(fromCurrency, toCurrency)
            .Returns(exchangeRate);

        // Act
        var result = currencyCalculator.Calculate(fromMoney.Value, toCurrency);

        // Assert
        Assert.That(result.IsSuccess, Is.True);
        Assert.That(result.Value.Amount, Is.EqualTo(expectedResult));
    }

    [Test]
    public void Given_RateCouldNotBeProvided_ReturnsFailure()
    {
        // Arrange
        const string fromCurrency = "USD";
        const string toCurrency = "DKK";
        const decimal validAmount = 100;
        var currencyCalculator = new CurrencyCalculatorService(_mockCurrencyRateProvider);
        var fromMoney = Money.Create(fromCurrency, validAmount);
        
        _mockCurrencyRateProvider
            .GetRate(fromMoney.Value.Currency, toCurrency)
            .Returns((decimal?)null);

        // Act
        var result = currencyCalculator.Calculate(fromMoney.Value, toCurrency);

        // Assert
        Assert.That(result.IsSuccess, Is.False);
        Assert.That(result.Error, Is.EqualTo(ErrorMessages.ExchangeRateNotFound(fromCurrency, toCurrency)));
    }

    [Test]
    public void Given_FromAndToCurrenciesAreTheSame_ReturnsOriginalAmount()
    {
        // Arrange
        const string fromCurrency = "DKK";
        const string toCurrency = "DKK";
        const decimal validAmount = 100;
        var currencyCalculator = new CurrencyCalculatorService(_mockCurrencyRateProvider);
        var fromMoney = Money.Create(fromCurrency, validAmount);
        
        _mockCurrencyRateProvider
            .GetRate(fromMoney.Value.Currency, toCurrency)
            .Returns((decimal?)null);

        // Act
        var result = currencyCalculator.Calculate(fromMoney.Value, toCurrency);

        // Assert
        Assert.That(result.IsSuccess, Is.True);
        Assert.That(result.Value.Amount, Is.EqualTo(validAmount));
    }
}