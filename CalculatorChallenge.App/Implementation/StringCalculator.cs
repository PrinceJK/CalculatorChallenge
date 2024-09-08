using CalculatorChallenge.App.Interface;

namespace CalculatorChallenge.App.Implementation;
public class StringCalculator : ICalculator
{
    public int Add(string numbers)
    {
        if (string.IsNullOrWhiteSpace(numbers))
        {
            return 0;
        }

        // Handle custom delimiters and basic input validation
        string[] delimiters = new[] { ",", "\n" };
        string numberString = numbers;

        // Custom delimiters with "//" format
        if (numbers.StartsWith("//"))
        {
            var delimiterEndIndex = numbers.IndexOf('\n');
            var customDelimiter = numbers.Substring(2, delimiterEndIndex - 2);

            if (customDelimiter.StartsWith("[") && customDelimiter.EndsWith("]"))
            {
                // Handling delimiters of any length
                customDelimiter = customDelimiter.Substring(1, customDelimiter.Length - 2);
            }
            delimiters = new[] { customDelimiter };
            numberString = numbers.Substring(delimiterEndIndex + 1);
        }

        var splitNumbers = numberString.Split(delimiters, StringSplitOptions.None);
        int sum = 0;
        List<int> negatives = new List<int>();

        foreach (var number in splitNumbers)
        {
            if (int.TryParse(number, out int parsedNumber))
            {
                if (parsedNumber < 0)
                {
                    negatives.Add(parsedNumber);
                }
                if (parsedNumber <= 1000)
                {
                    sum += parsedNumber;
                }
            }
        }

        if (negatives.Count > 0)
        {
            throw new ArgumentException("Negatives not allowed: " + string.Join(",", negatives));
        }

        return sum;
    }
}
