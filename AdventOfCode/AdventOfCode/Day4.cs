namespace AdventOfCode;

public abstract class Day4
{
    public static int GetScratchcardPoints()
    {
        var ret = 0;

        var cards = File.ReadLines("../../../../AdventOfCode/Inputs/Day4.txt").ToList();

        foreach (var card in cards)
        {
            var game = card.Split(":")[1].Trim();
            var numbersSplits = game.Split("|");
            var winning = numbersSplits[0].Trim().Replace("  ", " ").Split(" ").Select(int.Parse);
            var actual = numbersSplits[1].Trim().Replace("  ", " ").Split(" ").Select(int.Parse);

            var goodNumbers = winning.Where(x => actual.Contains(x)).ToArray();
            switch (goodNumbers.Length)
            {
                case 0:
                    continue;
                case 1:
                    ret += 1;
                    continue;
                default:
                    ret += (int)Math.Pow(2, goodNumbers.Length - 1);
                    break;
            }
        }

        return ret;
    }
}