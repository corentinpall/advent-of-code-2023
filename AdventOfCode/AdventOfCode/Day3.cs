namespace AdventOfCode;

public abstract class Day3
{
    public static int GetPartNumber()
    {
        var gearsAndParts = new Dictionary<string, List<int>>();

        var lines = File.ReadLines("../../../../AdventOfCode/Inputs/Day3.txt").ToArray();

        for (var x = 0; x < lines.Length; x++)
        for (var y = 0; y < lines[x].Length; y++)
        {
            if (!char.IsDigit(lines[x][y])) continue;

            var startNumIndex = y;
            var endNumIndex = y;

            while (y < lines[x].Length - 1 && char.IsNumber(lines[x][y + 1]))
            {
                endNumIndex++;
                y++;
            }

            var number = lines[x][startNumIndex..(endNumIndex + 1)];

            var topPossible = x > 0;
            var leftPossible = startNumIndex > 0;
            var bottomPossible = x < lines.Length - 1;
            var rightPossible = endNumIndex < lines[x].Length - 1;

            var topLine = new List<(string, char)>();
            var leftCol = new List<(string, char)>();
            var bottomLine = new List<(string, char)>();
            var rightCol = new List<(string, char)>();

            if (topPossible)
                for (var i = startNumIndex; i < endNumIndex + 1; i++)
                    topLine.Add((GetCoordinates(x - 1, i), lines[x - 1][i]));

            if (leftPossible)
            {
                if (topPossible)
                    leftCol.Add((GetCoordinates(x - 1, startNumIndex - 1), lines[x - 1][startNumIndex - 1]));

                if (bottomPossible)
                    leftCol.Add((GetCoordinates(x + 1, startNumIndex - 1), lines[x + 1][startNumIndex - 1]));

                leftCol.Add((GetCoordinates(x, startNumIndex - 1), lines[x][startNumIndex - 1]));
            }

            if (bottomPossible)
                for (var i = startNumIndex; i < endNumIndex + 1; i++)
                    bottomLine.Add((GetCoordinates(x + 1, i), lines[x + 1][i]));

            if (rightPossible)
            {
                if (topPossible) rightCol.Add((GetCoordinates(x - 1, endNumIndex + 1), lines[x - 1][endNumIndex + 1]));

                if (bottomPossible)
                    rightCol.Add((GetCoordinates(x + 1, endNumIndex + 1), lines[x + 1][endNumIndex + 1]));

                rightCol.Add((GetCoordinates(x, endNumIndex + 1), lines[x][endNumIndex + 1]));
            }

            foreach (var gear in topLine.Where(x => x.Item2 == '*')) AddGearsAndParts(gear.Item1, number);

            foreach (var gear in leftCol.Where(x => x.Item2 == '*')) AddGearsAndParts(gear.Item1, number);

            foreach (var gear in rightCol.Where(x => x.Item2 == '*')) AddGearsAndParts(gear.Item1, number);

            foreach (var gear in bottomLine.Where(x => x.Item2 == '*')) AddGearsAndParts(gear.Item1, number);
        }

        gearsAndParts = gearsAndParts.Where(x => x.Value.Count > 1).ToDictionary();

        return gearsAndParts.Values.Sum(partsLists => partsLists.Aggregate(1, (current, part) => current * part));

        void AddGearsAndParts(string coordinates, string number)
        {
            if (gearsAndParts!.TryGetValue(coordinates, out var value))
                value.Add(int.Parse(number));
            else
                gearsAndParts.Add(coordinates, [int.Parse(number)]);
        }
    }

    private static string GetCoordinates(int x, int y)
    {
        return $"{x},{y}";
    }
}