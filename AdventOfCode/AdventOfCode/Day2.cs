namespace AdventOfCode;

public abstract class Day2
{
    public static int GetPossibleGames()
    {
        var ret = 0;
        var lines = File.ReadLines("../../../../AdventOfCode/Inputs/Day2.txt");

        foreach (var gameString in lines)
        {
            var maxRed = 0;
            var maxGreen = 0;
            var maxBlue = 0;

            var gameSplits = gameString.Split(":");
            var sets = gameSplits[1].Replace(" ", string.Empty).Split(";");

            foreach (var colorsSet in sets)
            {
                var colorsSplits = colorsSet.Split(",");
                var redCubes = colorsSplits.SingleOrDefault(x => x.Contains("red"));
                var greenCubes = colorsSplits.SingleOrDefault(x => x.Contains("green"));
                var blueCubes = colorsSplits.SingleOrDefault(x => x.Contains("blue"));

                if (redCubes != null)
                {
                    var setReds = int.Parse(string.Concat(redCubes.Where(char.IsDigit)));
                    maxRed = setReds > maxRed
                        ? setReds
                        : maxRed;
                }

                if (greenCubes != null)
                {
                    var setGreens = int.Parse(string.Concat(greenCubes.Where(char.IsDigit)));
                    maxGreen = setGreens > maxGreen
                        ? setGreens
                        : maxGreen;
                }

                if (blueCubes == null) continue;
                var setBlues = int.Parse(string.Concat(blueCubes.Where(char.IsDigit)));
                maxBlue = setBlues > maxBlue
                    ? setBlues
                    : maxBlue;
            }

            ret += maxRed * maxGreen * maxBlue;
        }

        return ret;
    }
}