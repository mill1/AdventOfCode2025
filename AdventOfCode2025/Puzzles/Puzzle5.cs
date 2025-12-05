using AdventOfCode2025.Puzzles.Interfaces;

namespace AdventOfCode2025.Puzzles
{
    public static class Puzzle5Extensions
    {
        public static bool IsBetween(this long value, long min, long max)
            => value >= min && value <= max;
    }

    public class Puzzle5 : IPuzzle
    {
        public void Part1(bool useExample)
        {
            var lines = useExample ? GetExampleData() : File.ReadAllLines(@"Puzzles\Input\InputPuzzle5.txt").ToList();

            int divider = lines.IndexOf("");

            // Ranges as tuples
            var idRanges = lines
                .Take(divider)
                .Select(r =>
                {
                    var p = r.Split('-');
                    return (Min: long.Parse(p[0]), Max: long.Parse(p[1]));
                })
                .ToList();

            var ids = lines
                .Skip(divider + 1)
                .Select(long.Parse)
                .ToList();

            int sum = ids.Count(id => idRanges.Any(r => id.IsBetween(r.Min, r.Max)));

            Console.WriteLine($"Sum: {sum}");
        }

        private static List<string> GetExampleData() => new()
        {
            "3-5",
            "10-14",
            "16-20",
            "12-18",
            "",
            "1",
            "5",
            "8",
            "11",
            "17",
            "32",
        };

        public void Part2(bool useExample)
        {
            throw new NotImplementedException();
        }
    }
}
