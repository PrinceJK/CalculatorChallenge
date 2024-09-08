// See https://aka.ms/new-console-template for more information
using CalculatorChallenge.App.Implementation;
using CalculatorChallenge.App.Interface;
using Microsoft.Extensions.DependencyInjection;

var serviceProvider = new ServiceCollection()
            .AddTransient<ICalculator, StringCalculator>()
            .BuildServiceProvider();

var calculator = serviceProvider.GetService<ICalculator>();
Console.WriteLine("Enter numbers: ");
var input = Console.ReadLine();
try
{
    var result = calculator.Add(input);
    Console.WriteLine($"Result: {result}");
}
catch (ArgumentException ex)
{
    Console.WriteLine(ex.Message);
}
