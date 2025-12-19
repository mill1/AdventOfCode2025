using AdventOfCode2025.Puzzles.Interfaces;

namespace AdventOfCode2025.Puzzles
{
    public class Puzzle7 : IPuzzle
    {

        public void Part1(bool useExample)
        {
            Console.WriteLine($"Total: {Solve(useExample, true)}");
        }
        public void Part2(bool useExample)
        {
            Console.WriteLine($"Total: {Solve(useExample, false)}");
        }

        private long Solve(bool useExample, bool part1)
        {
            long totalPart1 = 0L;
            long totalPart2 = 0L;

            var grid = useExample
                            ? GetExampleData().Select(line => line.ToCharArray()).ToList()
                            : File.ReadAllLines(this.GetPathInputFile()).Select(line => line.ToCharArray()).ToList();

            var rowCounts = new long[grid.Count];

            // Find starting position
            int startIndex = Array.IndexOf(grid[0], 'S');

            rowCounts[startIndex] = 1;

            for (var row = 1; row < grid.Count; row++)
            {
                for (var col = 0; col < grid[0].Length; col++)
                {
                    if (grid[row][col] == '^')
                    {
                        var count = rowCounts[col];
                        rowCounts[col - 1] += count;
                        rowCounts[col] = 0;
                        rowCounts[col + 1] += count;

                        if (count > 0)
                            totalPart1++;
                    }
                }
            }

            foreach (var count in rowCounts)
                totalPart2 += count;

            return part1 ? totalPart1 : totalPart2;
        }

        private static List<string> GetExampleData()
        {
            return new List<string>
            {
                ".......S.......",
                "...............",
                ".......^.......",
                "...............",
                "......^.^......",
                "...............",
                ".....^.^.^.....",
                "...............",
                "....^.^...^....",
                "...............",
                "...^.^...^.^...",
                "...............",
                "..^...^.....^..",
                "...............",
                ".^.^.^.^.^...^.",
                "..............."
            };
        }
    }
}
