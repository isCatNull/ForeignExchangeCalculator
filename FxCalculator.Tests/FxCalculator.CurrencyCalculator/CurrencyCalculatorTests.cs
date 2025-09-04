using FxCalculator.Core.Entities;
using FxCalculator.Core.Interfaces;
using FxCalculator.Core.Shared;
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
        const string toCurrency = "DKK";
        const decimal validAmount = 100;
        var currencyCalculator = new CurrencyCalculatorService(_mockCurrencyRateProvider);
        var fromMoney = Money.Create(toCurrency, validAmount);
        
        _mockCurrencyRateProvider
            .GetRate(fromMoney.Value.Currency, toCurrency)
            .Returns(exchangeRate);

        // Act
        var result = currencyCalculator.Calculate(fromMoney, toCurrency);

        // Assert
        Assert.That(result.Success, Is.True);
        Assert.That(result.Value.Amount, Is.EqualTo(expectedResult));
    }

    [Test]
    public void Given_RateCouldNotBeProvided_ReturnsFailure()
    {
        // Arrange
        const string toCurrency = "DKK";
        const decimal validAmount = 100;
        var currencyCalculator = new CurrencyCalculatorService(_mockCurrencyRateProvider);
        var fromMoney = Money.Create(toCurrency, validAmount);
        
        _mockCurrencyRateProvider
            .GetRate(fromMoney.Value.Currency, toCurrency)
            .Returns((decimal?)null);

        // Act
        var result = currencyCalculator.Calculate(fromMoney, toCurrency);

        // Assert
        Assert.That(result.IsSuccess, Is.False);
        Assert.That(result.Error, Is.EqualTo(ErrorMessages.ExchangeRateNotFound(fromMoney.Value.Currency, toCurrency)));
    }
}