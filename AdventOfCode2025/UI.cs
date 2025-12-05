using AdventOfCode2025.Puzzles;

namespace AdventOfCode2025
{
    public class UI
    {
        private readonly Dictionary<string, Action<bool>> _menu;

        public UI()
        {
            _menu = new Dictionary<string, Action<bool>>(StringComparer.OrdinalIgnoreCase)
            {
                ["1a"] = new Puzzle1().Part1,
                ["1b"] = new Puzzle1().Part2,
                ["2a"] = new Puzzle2().Part1,
                ["2b"] = new Puzzle2().Part2,
                ["3a"] = new Puzzle3().Part1,
                ["3b"] = new Puzzle3().Part2,
                ["4a"] = new Puzzle4().Part1,
                ["4b"] = new Puzzle4().Part2,
                ["5a"] = new Puzzle5().Part1,
                ["5b"] = new Puzzle5().Part2,
            };
        }

        public void Run()
        {
            while (true)
            {
                Console.WriteLine("\r\nWhich puzzle? (e.g. 2a). Available: " + string.Join(", ", _menu.Keys));
                Console.WriteLine("Or enter q to quit.");
                var answer = (Console.ReadLine() ?? "").Trim();

                if (string.Equals(answer, "q", StringComparison.OrdinalIgnoreCase))
                    return;

                if (!_menu.TryGetValue(answer, out var puzzleAction))
                {
                    WriteLineFormatted(ConsoleColor.DarkBlue,ConsoleColor.Yellow, "Invalid option");
                    continue;
                }

                Console.Write("Example data? (y/n) ");
                bool useExample = string.Equals(Console.ReadLine() ?? "", "y", StringComparison.OrdinalIgnoreCase);
                RunPuzzle(puzzleAction, useExample);
            }
        }

        private void RunPuzzle(Action<bool> puzzleAction, bool useExample)
        {
            try
            {
                puzzleAction(useExample);
            }
            catch (Exception ex)
            {
                WriteLineFormatted(ConsoleColor.Black, ConsoleColor.Red, $"Error while running puzzle: {ex.Message}");
            }
        }

        private void WriteLineFormatted(ConsoleColor backgroundColor, ConsoleColor foregroundColor, string? value)
        {
            Console.BackgroundColor = backgroundColor;
            Console.ForegroundColor = foregroundColor;
            Console.WriteLine(value);
            Console.ResetColor();
        }
    }
}
