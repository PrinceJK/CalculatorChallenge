using CalculatorChallenge.App.Interface;
using System.Text.RegularExpressions;

namespace CalculatorChallenge.App.Implementation;
public class StringCalculator : ICalculator
{
    public int AddUpToTwoNumbers(string numbers)
    {
        if (string.IsNullOrEmpty(numbers))
            return 0;

        string[] parts = numbers.Split(',');
        if (parts.Length > 2)
            throw new ArgumentException("Only up to 2 numbers are allowed.");

        int sum = 0;
        foreach (var part in parts)
        {
            if (int.TryParse(part, out int number))
            {
                sum += number;
            }
        }
        return sum;
    }

    public int AddRemoveMaxConstraintForNumbers(string numbers)
    {
        if (string.IsNullOrEmpty(numbers))
        {
            return 0;
        }

        var numArray = numbers.Split(',').Select(n => int.TryParse(n, out int result) ? result : 0).ToArray();

        return numArray.Sum();
    }
    public int AddSupportNewlineAsADelimiter(string numbers)
    {
        if (string.IsNullOrEmpty(numbers))
        {
            return 0;
        }

        var delimiters = new[] { ',', '\n' };
        var numArray = numbers.Split(delimiters).Select(n => int.TryParse(n, out int result) ? result : 0).ToArray();

        return numArray.Sum();
    }

    public int AddDenyNegativeNumbers(string numbers)
    {
        if (string.IsNullOrEmpty(numbers))
        {
            return 0;
        }

        var delimiters = new[] { ',', '\n' };
        var numArray = numbers.Split(delimiters).Select(n => int.TryParse(n, out int result) ? result : 0).ToArray();

        var negativeNumbers = numArray.Where(n => n < 0).ToArray();
        if (negativeNumbers.Any())
        {
            throw new ArgumentException("Negative numbers not allowed: " + string.Join(", ", negativeNumbers));
        }

        return numArray.Sum();
    }

    public int AddIgnoreNumbersGreaterThan1000(string numbers)
    {
        if (string.IsNullOrEmpty(numbers))
        {
            return 0;
        }

        var delimiters = new[] { ',', '\n' };
        var numArray = numbers.Split(delimiters).Select(n => int.TryParse(n, out int result) ? result : 0).Where(n => n <= 1000).ToArray();

        var negativeNumbers = numArray.Where(n => n < 0).ToArray();
        if (negativeNumbers.Any())
        {
            throw new ArgumentException("Negative numbers not allowed: " + string.Join(", ", negativeNumbers));
        }

        return numArray.Sum();
    }

    public int AddSupportCustomDelimiters(string numbers)
    {
        if (string.IsNullOrEmpty(numbers))
        {
            return 0;
        }

        string[] delimiters = { ",", "\n" };
        if (numbers.StartsWith("//"))
        {
            var delimiterEndIndex = numbers.IndexOf('\n');
            var delimiter = numbers.Substring(2, delimiterEndIndex - 2);
            delimiters = new[] { delimiter };
            numbers = numbers.Substring(delimiterEndIndex + 1);
        }

        var numArray = numbers.Split(delimiters, StringSplitOptions.None).Select(n => int.TryParse(n, out int result) ? result : 0).Where(n => n <= 1000).ToArray();

        var negativeNumbers = numArray.Where(n => n < 0).ToArray();
        if (negativeNumbers.Any())
        {
            throw new ArgumentException("Negative numbers not allowed: " + string.Join(", ", negativeNumbers));
        }

        return numArray.Sum();
    }

    public int AddSupportCustomDelimitersOfAnyLength(string numbers)
    {
        if (string.IsNullOrEmpty(numbers))
        {
            return 0;
        }

        var delimiters = new List<string> { ",", "\n" };
        if (numbers.StartsWith("//"))
        {
            var delimiterEndIndex = numbers.IndexOf('\n');
            var delimiterPart = numbers.Substring(2, delimiterEndIndex - 2);
            numbers = numbers.Substring(delimiterEndIndex + 1);

            if (delimiterPart.StartsWith("[") && delimiterPart.EndsWith("]"))
            {
                delimiters.AddRange(Regex.Matches(delimiterPart, @"\[(.*?)\]").Cast<Match>().Select(m => m.Groups[1].Value));
            }
            else
            {
                delimiters.Add(delimiterPart);
            }
        }

        var numArray = numbers.Split(delimiters.ToArray(), StringSplitOptions.None)
                              .Select(n => int.TryParse(n, out int result) ? result : 0)
                              .Where(n => n <= 1000)
                              .ToArray();

        var negativeNumbers = numArray.Where(n => n < 0).ToArray();
        if (negativeNumbers.Any())
        {
            throw new ArgumentException("Negative numbers not allowed: " + string.Join(", ", negativeNumbers));
        }

        return numArray.Sum();
    }


    public int AddSupportMultipleDelimitersOfAnyLength(string numbers)
    {
        if (string.IsNullOrEmpty(numbers))
        {
            return 0;
        }

        var delimiters = new List<string> { ",", "\n" };
        if (numbers.StartsWith("//"))
        {
            var delimiterEndIndex = numbers.IndexOf('\n');
            var delimiterSection = numbers.Substring(2, delimiterEndIndex - 2);
            delimiters.AddRange(delimiterSection.Split(new[] { '[', ']' }, StringSplitOptions.RemoveEmptyEntries));
            numbers = numbers.Substring(delimiterEndIndex + 1);
        }

        var numArray = numbers.Split(delimiters.ToArray(), StringSplitOptions.None).Select(n => int.TryParse(n, out int result) ? result : 0).Where(n => n <= 1000).ToArray();

        var negativeNumbers = numArray.Where(n => n < 0).ToArray();
        if (negativeNumbers.Any())
        {
            throw new ArgumentException("Negative numbers not allowed: " + string.Join(", ", negativeNumbers));
        }

        return numArray.Sum();
    }



    public int Calculate(string input)
    {
        // Parse the input string to extract numbers and delimiters
        var parsedNumbers = ParseInput(input);

        // Filter out invalid numbers (greater than 1000) and negative numbers
        var validNumbers = parsedNumbers
            .Where(num => num <= 1000 && num >= 0)
            .ToList();

        // Check for negative numbers and throw an exception if found
        if (parsedNumbers.Any(num => num < 0))
        {
            throw new ArgumentException($"Negative numbers are not allowed: {string.Join(", ", parsedNumbers.Where(num => num < 0))}");
        }

        // Calculate the sum of valid numbers
        var result = validNumbers.Sum();

        return result;
    }

    private IEnumerable<int> ParseInput(string input)
    {
        // Extract the custom delimiter (if present)
        var delimiter = ExtractDelimiter(input);

        // Split the input string based on the delimiter
        var numbers = input.Substring(delimiter.Length)
            .Split(delimiter.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

        // Parse the numbers and return them as an IEnumerable
        return numbers.Select(num => int.TryParse(num, out var parsedNum) ? parsedNum : 0);
    }

    private string ExtractDelimiter(string input)
    {
        // Check for custom delimiter format
        if (input.StartsWith("//"))
        {
            // Extract the delimiter between square brackets or single character
            //var delimiterMatch = input.Match(@"//\[(.*)\]|\/\/(.*)");
            var delimiterMatch = Regex.Match(input, @"//\[(.*)\]|\/\/(.*)");

            if (delimiterMatch.Success)
            {
                return delimiterMatch.Groups[1].Value + delimiterMatch.Groups[2].Value;
            }
        }

        // Default to comma and newline as delimiters
        return "\n,";
    }
}