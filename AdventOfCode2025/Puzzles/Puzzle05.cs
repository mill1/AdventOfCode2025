using AdventOfCode2025.Puzzles.Interfaces;

namespace AdventOfCode2025.Puzzles
{
    public class Puzzle05 : IPuzzle
    {
        public void Part1(bool useExample)
        {
            var lines = useExample ? GetExampleData() : File.ReadAllLines(this.GetPathInputFile()).ToList();
            int divider = lines.IndexOf("");            
            var idRanges = GetIdRanges(lines, divider);

            var ids = lines
                .Skip(divider + 1)
                .Select(long.Parse)
                .ToList();

            int sum = ids.Count(id => idRanges.Any(r => id.IsBetween(r.Min, r.Max)));

            Console.WriteLine($"Sum: {sum}");
        }

        public void Part2(bool useExample)
        {
            var lines = useExample ? GetExampleData() : File.ReadAllLines(this.GetPathInputFile()).ToList();
            int divider = lines.IndexOf("");
            var idRanges = GetIdRanges(lines, divider);

            Console.WriteLine($"Count: {CountUniqueValues(idRanges)}");
        }

        private long CountUniqueValues(List<(long Min, long Max)> ranges)
        {
            var ordered = ranges.OrderBy(r => r.Min).ToList();
            long mergedMin = ordered[0].Min;
            long mergedMax = ordered[0].Max;
            long total = 0;

            foreach (var (min, max) in ordered.Skip(1))
            {
                if (min <= mergedMax + 1)
                {
                    // Overlaps; extend the current merged range
                    mergedMax = Math.Max(mergedMax, max);
                }
                else
                {
                    // No overlap; finalize the previous chunk
                    total += (mergedMax - mergedMin + 1);
                    mergedMin = min;
                    mergedMax = max;
                }
            }            
            total += (mergedMax - mergedMin + 1);

            return total;
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

        private static List<(long Min, long Max)> GetIdRanges(List<string> lines, int divider)
        {
            // Return ranges as tuples
            return lines.Take(divider)
                .Select(r =>
                {
                    var p = r.Split('-');
                    return (Min: long.Parse(p[0]), Max: long.Parse(p[1]));
                }).ToList();
        }
    }

    public static class Puzzle5Extensions
    {
        public static bool IsBetween(this long value, long min, long max)
            => value >= min && value <= max;
    }
}
