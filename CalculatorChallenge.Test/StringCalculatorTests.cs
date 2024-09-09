using CalculatorChallenge.App.Implementation;
using CalculatorChallenge.App.Interface;
using Xunit;

namespace CalculatorChallenge.Test;

public class StringCalculatorTests
{
    private readonly ICalculator _calculator;

    public StringCalculatorTests()
    {
        _calculator = new StringCalculator();
    }

    [Fact]
    public void AddUpToTwoNumbers()
    {
        var result1 = _calculator.AddUpToTwoNumbers("20");
        var result2 = _calculator.AddUpToTwoNumbers("1,5000");
        var result3 = _calculator.AddUpToTwoNumbers("4,-3");
        var result4 = _calculator.AddUpToTwoNumbers(string.Empty);
        var result5 = _calculator.AddUpToTwoNumbers("5,tytyt");
        Assert.Equal(20, result1);
        Assert.Equal(5001, result2);
        Assert.Equal(1, result3);
        Assert.Equal(0, result4);
        Assert.Equal(5, result5);
    }

    [Fact]
    public void AddRemoveMaxConstraintForNumbers()
    {
        var result = _calculator.AddRemoveMaxConstraintForNumbers("1,2,3,4,5,6,7,8,9,10,11,12");
        Assert.Equal(78, result);
    }

    [Fact]
    public void AddSupportNewlineAsADelimiter()
    {
        var result = _calculator.AddSupportNewlineAsADelimiter("1\n2,3");
        Assert.Equal(6, result);
    }

    [Fact]
    public void AddDenyNegativeNumbers_NegativeNumbers_ThrowsException()
    {
        var ex = Assert.Throws<ArgumentException>(() => _calculator.AddDenyNegativeNumbers("1,-2,3,-4, -9"));
        Assert.Equal("Negative numbers not allowed: -2, -4, -9", ex.Message);
    }

    [Fact]
    public void AddIgnoreNumbersGreaterThan1000()
    {
        var result = _calculator.AddIgnoreNumbersGreaterThan1000("2,1001,6");
        Assert.Equal(8, result);
    }

    [Fact]
    public void AddSupportCustomDelimiters()
    {
        var result1 = _calculator.AddSupportCustomDelimiters("//#\n2#5");
        var result2 = _calculator.AddSupportCustomDelimiters("//,\n2,ff,100");
        Assert.Equal(7, result1);
        Assert.Equal(102, result2);
    }

    [Fact]
    public void AddSupportCustomDelimitersOfAnyLength()
    {
        var result = _calculator.AddSupportCustomDelimitersOfAnyLength("//[***]\n11***22***33");
        Assert.Equal(66, result);
    }



    [Fact]
    public void AddSupportMultipleDelimitersOfAnyLength()
    {
        var result = _calculator.AddSupportMultipleDelimitersOfAnyLength("//[*][!!][r9r]\n11r9r22*hh*33!!44");
        Assert.Equal(110, result);
    }
}
