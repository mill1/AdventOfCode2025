using AdventOfCode2025.Puzzles.Interfaces;

namespace AdventOfCode2025.Puzzles
{
    public class Puzzle7 : IPuzzle
    {

        public void Part1(bool useExample)
        {
            Console.WriteLine($"Total: {Solve(useExample).part1}");
        }
        public void Part2(bool useExample)
        {
            Console.WriteLine($"Total: {Solve(useExample).part2}");
        }

        private (long part1, long part2) Solve(bool useExample)
        {
            var grid = LoadGrid(useExample);
            var state = InitializeState(grid);

            long part1 = Simulate(grid, state);
            long part2 = state.RowCounts.Sum();

            return (part1, part2);
        }

        private List<char[]> LoadGrid(bool useExample)
        {
            var lines = useExample
                ? GetExampleData()
                : File.ReadAllLines(this.GetPathInputFile()).ToList();

            return lines.Select(l => l.ToCharArray()).ToList();
        }

        private SimulationState InitializeState(List<char[]> grid)
        {
            int startCol = Array.IndexOf(grid[0], 'S');

            var state = new SimulationState(grid[0].Length);
            state.RowCounts[startCol] = 1;

            return state;
        }

        private long Simulate(List<char[]> grid, SimulationState state)
        {
            long usedSplitters = 0;

            for (int row = 1; row < grid.Count; row++)
            {
                for (int col = 0; col < grid[row].Length; col++)
                {
                    if (grid[row][col] == '^')
                        usedSplitters += SplitAt(state.RowCounts, col);
                }
            }

            return usedSplitters;
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

        private class SimulationState
        {
            public long[] RowCounts { get; }

            public SimulationState(int width)
            {
                RowCounts = new long[width];
            }
        }
    }
}
