using System.Text;

namespace AdventOfCode;

public static class Day1
{
    private static readonly Dictionary<string, string> Numbers = new()
    {
        { "one", "1" }, { "two", "2" }, { "three", "3" }, { "four", "4" }, { "five", "5" }, { "six", "6" },
        { "seven", "7" }, { "eight", "8" }, { "nine", "9" }
    };
     public static int GetCoordinates()
    {
        var lines = File.ReadLines("../../../../AdventOfCode/Inputs/Day1.txt");
        var digitsArr = lines.Select(line => ReplaceNumbers(line).Where(char.IsDigit).ToArray());
        var firstLast = digitsArr.Select(digits => digits.Length >= 2
                ? int.Parse(string.Concat(digits.First(), digits.Last()))
                : int.Parse(string.Concat(digits.First(), digits.First())))
            .ToArray();
        return firstLast.Sum();
    }

    private static string ReplaceNumbers(string s)
    {
        var sb = new StringBuilder(s);
        if (sb.ToString().All(char.IsDigit))
        {
            return sb.ToString();
        }
        for (var i = 0; i < sb.Length - 2; i++)
        {
            if (char.IsNumber(sb[i])) continue;
            SearchNumberOfSize(ref i, ref sb, 3);
            SearchNumberOfSize(ref i, ref sb, 4);
            SearchNumberOfSize(ref i, ref sb, 5); 
        }

        return sb.ToString();

        void SearchNumberOfSize(ref int index, ref StringBuilder sb, int size)
        {
            if (index + size > sb.Length) return;
            var ss = sb.ToString().Substring(index, size);
            if (!Numbers.ContainsKey(ss)) return;
            sb.Replace(ss, $"{ss.First()}{Numbers.Single(x => x.Key == ss).Value}{ss.Last()}", index, size);
            index++;
        }
    }
}