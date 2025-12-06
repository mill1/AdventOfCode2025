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
                ["6a"] = new Puzzle6().Part1,
                ["6b"] = new Puzzle6().Part2,
            };
        }

        public void Run()
        {
            PrintBanner();

            while (true)
            {
                WriteFormatted(ConsoleColor.Black, ConsoleColor.Yellow, "\r\nWhich puzzle? (e.g. 2a for Day 2 part 1)." +
                    "\r\nAvailable: " + string.Join(", ", _menu.Keys));
                WriteFormatted(ConsoleColor.Black, ConsoleColor.Yellow, "Or enter h for help or q to quit.");
                
                var answer = (Console.ReadLine() ?? "").Trim();

                switch (answer)
                {
                    case "q":
                        return;
                    case "h":
                        Console.WriteLine("https://www.youtube.com/watch?v=2Q_ZzBGPdqE");
                        break;
                    default:
                        if (!_menu.TryGetValue(answer, out var puzzleAction))
                        {
                            WriteFormatted(ConsoleColor.White, ConsoleColor.DarkMagenta, "Invalid option");
                            continue;
                        }
                        WriteFormatted(ConsoleColor.Black, ConsoleColor.Yellow, "Example data? (y/n)");
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
                WriteFormatted(ConsoleColor.Black, ConsoleColor.Red, $"Error while running puzzle: {ex.Message}");               
            }
        }

        private void PrintBanner()
        {
            const int Width = 41;

            for (int i = 0; i < Width; i++)
                WriteFormatted(i % 5 > 2 ? ConsoleColor.DarkRed : ConsoleColor.DarkBlue, ConsoleColor.White, " ", false);            
            Console.WriteLine();

            foreach (var line in new[] { "", "Advent of Code 2025", "", "*** Solutions ***", "" })
                WriteFormatted(ConsoleColor.DarkBlue, ConsoleColor.Yellow, line.PadLeft((Width + line.Length) / 2).PadRight(Width));

            for (int i = 0; i < Width; i++)
                WriteFormatted(i % 5 < 2 ? ConsoleColor.DarkGreen : ConsoleColor.DarkBlue, ConsoleColor.White, " ", false);
        }

        private void WriteFormatted(ConsoleColor backgroundColor, ConsoleColor foregroundColor, string? value, bool writeLine = true)
        {
            Console.BackgroundColor = backgroundColor;
            Console.ForegroundColor = foregroundColor;

            if(writeLine)
                Console.WriteLine(value);
            else
                Console.Write(value);

            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
