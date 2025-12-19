using AdventOfCode2025.Puzzles.Interfaces;

namespace AdventOfCode2025.Puzzles
{
    public class Puzzle7 : IPuzzle
    {
        public void Part1(bool useExample)
            => Console.WriteLine($"Total: {Solve(useExample).part1}");

        public void Part2(bool useExample)
            => Console.WriteLine($"Total: {Solve(useExample).part2}");

        private (long part1, long part2) Solve(bool useExample)
        {
            var grid = LoadGrid(useExample);
            var rowCounts = InitializeRowCounts(grid);

            long part1 = Simulate(grid, rowCounts);
            long part2 = rowCounts.Sum();

            return (part1, part2);
        }

        private List<char[]> LoadGrid(bool useExample)
        {
            var lines = useExample
                ? GetExampleData()
                : File.ReadAllLines(this.GetPathInputFile()).ToList();

            return lines.Select(l => l.ToCharArray()).ToList();
        }

        private long[] InitializeRowCounts(List<char[]> grid)
        {
            int startCol = Array.IndexOf(grid[0], 'S');

            var rowCounts = new long[grid[0].Length];
            rowCounts[startCol] = 1;

            return rowCounts;
        }

        private long Simulate(List<char[]> grid, long[] rowCounts)
        {
            long splitCount = 0;

            for (int row = 1; row < grid.Count; row++)
            {
                for (int col = 0; col < grid[row].Length; col++)
                {
                    if (grid[row][col] == '^')
                        splitCount += SplitAt(rowCounts, col);
                }
            }

            return splitCount;
        }

        private long SplitAt(long[] counts, int col)
        {
            long count = counts[col];

            if (count == 0)
                return 0;

            counts[col - 1] += count;
            counts[col + 1] += count;
            counts[col] = 0;

            return 1;
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
