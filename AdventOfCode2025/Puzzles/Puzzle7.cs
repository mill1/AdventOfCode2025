using AdventOfCode2025.Puzzles.Interfaces;

namespace AdventOfCode2025.Puzzles
{
    public class Puzzle7 : IPuzzle
    {
        private List<char[]> _grid;
        private int _total;

        public void Part1(bool useExample)
        {
            _grid = useExample 
                ? GetExampleData().Select(line => line.ToCharArray()).ToList() 
                : File.ReadAllLines(this.GetPathInputFile()).Select(line => line.ToCharArray()).ToList();

            Console.WriteLine(this.ToString());

            // Find starting position
            int startIndex = Array.IndexOf(_grid[0], 'S');
            // Initialize second line
            _grid[1][startIndex] = '|';

            Console.WriteLine(this.ToString());

            for (int row = 2; row < _grid.Count; row++)
            {
                ProcessBeams(row);

                if(useExample)
                    Console.WriteLine(this.ToString());
            }

            Console.WriteLine(this.ToString());
        }

        private void ProcessBeams(int row)
        {
            for (int col = 0; col < _grid[row].Length ; col++)
            {
                if (_grid[row - 1][col] == '|')
                {
                    // Did we hit a splitter?
                    if (_grid[row][col] == '^')
                    {
                        _total++;

                        // Split beam.
                        if (col > 0)
                            _grid[row][col - 1] = '|';

                        if (col < _grid[row].Length - 1)
                            _grid[row][col + 1] = '|';
                    }
                    else 
                    {
                        _grid[row][col] = '|';
                    }
                }
            }            
        }

        public void Part2(bool useExample)
        {
            throw new NotImplementedException();
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

        public override string? ToString()
        {
            // Convert List<char[]> to List<string>:
            var lines = _grid.Select(arr => new string(arr)).ToList();
            var matrix = string.Join(Environment.NewLine, lines);

            return $"{matrix}{Environment.NewLine}Total: {_total}{Environment.NewLine}";
        }
    }
}
