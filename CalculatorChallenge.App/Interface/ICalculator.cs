namespace CalculatorChallenge.App.Interface;
public interface ICalculator
{
    int AddUpToTwoNumbers(string numbers);
    //int Calculate(string numbers);
    int AddRemoveMaxConstraintForNumbers(string numbers);
    int AddSupportNewlineAsADelimiter(string numbers);
    int AddDenyNegativeNumbers(string numbers);
    int AddIgnoreNumbersGreaterThan1000(string numbers);
    int AddSupportCustomDelimiters(string numbers);
    int AddSupportCustomDelimitersOfAnyLength(string numbers);
    int AddSupportMultipleDelimitersOfAnyLength(string numbers);
}

