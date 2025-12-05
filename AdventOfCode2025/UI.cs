using AdventOfCode2025.Puzzles;

namespace AdventOfCode2025
{
    public class UI
    {
        private readonly Dictionary<string, Action<bool>> _menu;

        public UI()
        {
            PrintBanner();

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
                WriteLineFormatted(ConsoleColor.Black, ConsoleColor.Yellow, "\r\nWhich puzzle? (e.g. 2a for Day 2 part 1)." +
                    "\r\nAvailable: " + string.Join(", ", _menu.Keys));
                WriteLineFormatted(ConsoleColor.Black, ConsoleColor.Yellow, "Or enter h for help or q to quit.");
                
                var answer = (Console.ReadLine() ?? "").Trim();

                switch (answer)
                {
                    case "q":
                        return;
                    case "h":
                        Console.WriteLine("https://www.youtube.com/watch?v=2Q_ZzBGPdqE&list=RD2Q_ZzBGPdqE");
                        break;
                    default:
                        if (!_menu.TryGetValue(answer, out var puzzleAction))
                        {
                            WriteLineFormatted(ConsoleColor.White, ConsoleColor.DarkMagenta, "Invalid option");
                            continue;
                        }
                        WriteLineFormatted(ConsoleColor.Black, ConsoleColor.Yellow, "Example data? (y/n)");
                        bool useExample = string.Equals(Console.ReadLine() ?? "", "y", StringComparison.OrdinalIgnoreCase);
                        RunPuzzle(puzzleAction, useExample);
                        break;
                }
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

        private void PrintBanner()
        {
            const int Width = 41;

            var lines = new[] { "", "", "Advent of Code 2025", "", "*** Solutions ***", "", "" };

            foreach (var line in lines)
                WriteLineFormatted(ConsoleColor.DarkBlue, ConsoleColor.Cyan, line.PadLeft((Width + line.Length) / 2).PadRight(Width));
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
