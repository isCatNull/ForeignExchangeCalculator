using FxCalculator.Application;
using FxCalculator.Core.Entities;
using FxCalculator.Core.Interfaces;
using FxCalculator.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace FxCalculator.ConsoleApp;

class Program
{
    static int Main(string[] args)
    {
        PrintToConsole("Exchange <currency pair> <amount to exchange>");
        if (args.Length != 3)
        {
            PrintToConsole("Incorrect number of arguments provided. Expected: <currency pair> <amount to exchange>");
            return 1;
        }
        var input = args[0];
        var index = input.IndexOf('/');
        if (index == -1)
        {
            PrintToConsole("Please provide a valid currency exchange pair e.g. DKK/EUR");
            return 1;
        }

        if (!decimal.TryParse(args[1], out var amount))
        {
            PrintToConsole("Provided amount is not valid, please provide a numeric value to exchange");
            return 1;
        }
        
        var fromCurrency = input[..index].ToUpperInvariant();
        var toCurrency = input[(index + 1)..].ToUpperInvariant();

        var services = new ServiceCollection();
        services.AddTransient<ICurrencyCalculatorService, CurrencyCalculatorService>();
        services.AddTransient<ICurrencyRateProvider, StaticCurrencyRateProvider>();
        
        var serviceProvider = services.BuildServiceProvider();
        var currencyExchangeCalculator = serviceProvider.GetRequiredService<ICurrencyCalculatorService>();
        var fromMoney = Money.Create(fromCurrency, amount);
        
        if (!fromMoney.IsSuccess)
        {
            PrintToConsole(fromMoney.Error ?? "Unknown error occured while parsing money to exchange");
            return 1;
        }
        
        var result = currencyExchangeCalculator.ExchangeCurrency(fromMoney.Value, toCurrency);
        
        if (!result.IsSuccess)
        {
            PrintToConsole(result.Error ?? "Unknown error occured while converting currency");
            return 1;
        }

        Console.WriteLine(result.Value.Amount);
        return 0;
    }

    private static void PrintToConsole(string message)
    {
        Console.WriteLine(message);
    }
}