using FxCalculator.Infrastructure;
using NUnit.Framework;

namespace FxCalculator.Tests.FxCalculator.Infrastructure;

[TestFixture]
public class StaticCurrencyRateProviderTests
{
    private StaticCurrencyRateProvider _sut;
    private const string ValidEuroCurrency = "EUR";
    private const string ValidDanishCurrency = "DKK";
    private const string InvalidNonExistentCurrency = "aa";

    [SetUp]
    public void SetUp()
    {
        _sut = new StaticCurrencyRateProvider();
    }
    
    [Test]
    public void Given_CurrencyExists_ReturnsCurrencyRate()
    {
        // Arrange & Act
        var result = _sut.GetRate(ValidEuroCurrency, ValidDanishCurrency);

        // Assert
        Assert.That(result, Is.Not.Null);
    }

    [TestCase(InvalidNonExistentCurrency, ValidEuroCurrency)]
    [TestCase(ValidDanishCurrency, InvalidNonExistentCurrency)]
    [TestCase(InvalidNonExistentCurrency, InvalidNonExistentCurrency)]
    public void Given_CurrencyDoesNotExist_ReturnsNull(string fromCurrency, string toCurrency)
    {
        // Arrange & Act
        var result = _sut.GetRate(fromCurrency, toCurrency);

        // Assert
        Assert.That(result, Is.Null);
    }
}