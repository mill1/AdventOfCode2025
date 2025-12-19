using AdventOfCode2025.Puzzles;
using AdventOfCode2025.Puzzles.Interfaces;

namespace AdventOfCode2025
{
    public class UI
    {
        private readonly Dictionary<string, Action<bool>> _menu;

        public UI()
        {
            _menu = new Dictionary<string, Action<bool>>(StringComparer.OrdinalIgnoreCase);

            var puzzles = new Dictionary<int, IPuzzle>
            {
                [1] = new Puzzle1(),
                [2] = new Puzzle2(),
                [3] = new Puzzle3(),
                [4] = new Puzzle4(),
                [5] = new Puzzle5(),
                [6] = new Puzzle6(),
                [7] = new Puzzle7(),
            };

            foreach (var (day, puzzle) in puzzles)
                Register(day, puzzle);
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

        private void Register(int day, IPuzzle puzzle)
        {
            _menu[$"{day}a"] = puzzle.Part1;
            _menu[$"{day}b"] = puzzle.Part2;
        }
    }
}
