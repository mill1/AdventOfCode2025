using AdventOfCode2025.Puzzles.Interfaces;

namespace AdventOfCode2025.Puzzles
{
    public class Puzzle4 : IPuzzle
    {
        public void Part1(bool example)
        {
            // Example data
            var grid = GetExampleData();

            if (!example)
                grid = File.ReadAllLines(@"Puzzles\Input\InputPuzzle4.txt").ToList();

            var sum = 0;

            for (int i = 0; i < grid.Count; i++)
                for (int j = 0; j < grid[i].Length; j++)
                    if (grid[i][j] == '@')
                        if (CountAdjacentAt(grid, i, j) < 4)
                            sum++;

            Console.WriteLine($"Sum: {sum}");
        }

        private static List<string> GetExampleData()
        {
            return new List<string>
            {
                "..@@.@@@@.",
                "@@@.@.@.@@",
                "@@@@@.@.@@",
                "@.@@@@..@.",
                "@@.@@@@.@@",
                ".@@@@@@@.@",
                ".@.@.@.@@@",
                "@.@@@.@@@@",
                ".@@@@@@@@.",
                "@.@.@@@.@.",
            };
        }

        public void Part2(bool example)
        {
            // Example data
            var grid = GetExampleData();

            if (!example)
                grid = File.ReadAllLines(@"Puzzles\Input\InputPuzzle4.txt").ToList();

            int removedCount = 1;
            int sum = 0;

            while (removedCount > 0)
            {
                removedCount = 0;
                // Create a copy of the grid to avoid modifying while iterating
                var newGrid = new List<string>(grid);

                for (int i = 0; i < grid.Count; i++)
                {
                    char[] newRow = newGrid[i].ToCharArray();
                    for (int j = 0; j < grid[0].Length; j++)
                    {
                        if (grid[i][j] == '@' && CountAdjacentAt(grid, i, j) < 4)
                        {
                            newRow[j] = '.';
                            removedCount++;
                        }
                    }
                    newGrid[i] = new string(newRow);
                }

                // Print updated grid
                //foreach (var row in newGrid)
                //    Console.WriteLine(row);

                Console.WriteLine($"Removed: {removedCount}");
                sum += removedCount;
                grid = newGrid;
            }

            Console.WriteLine($"Total removed: {sum}");
        }

        int CountAdjacentAt(List<string> grid, int row, int col)
        {
            int count = 0;
            int[] dRow = { -1, -1, -1, 0, 0, 1, 1, 1 };
            int[] dCol = { -1, 0, 1, -1, 1, -1, 0, 1 };

            for (int i = 0; i < 8; i++)
            {
                int newRow = row + dRow[i];
                int newCol = col + dCol[i];

                if (newRow >= 0 && newRow < grid.Count && newCol >= 0 && newCol < grid[0].Length)
                {
                    if (grid[newRow][newCol] == '@')
                        count++;
                }
            }

            return count;
        }
    }
}
