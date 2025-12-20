using AdventOfCode2025.Puzzles.Interfaces;

namespace AdventOfCode2025
{
    public class UI
    {
        private readonly Dictionary<string, Action<bool>> _menu;

        public UI()
        {
            _menu = new Dictionary<string, Action<bool>>(StringComparer.OrdinalIgnoreCase);

            var puzzleTypes = typeof(IPuzzle).Assembly.GetTypes()
                .Where(t => typeof(IPuzzle).IsAssignableFrom(t) && !t.IsAbstract && t.Name.StartsWith("Puzzle"))
                .Select(t => new
                {
                    Type = t,
                    Day = ParseDay(t.Name)
                })
                .Where(x => x.Day.HasValue)
                .OrderBy(x => x.Day.Value);

            foreach (var puzzle in puzzleTypes)
            {
                var instance = (IPuzzle)Activator.CreateInstance(puzzle.Type)!;
                Register(puzzle.Day!.Value, instance);
            }
        }

        public void Run()
        {
            PrintBanner();

            while (true)
            {
                PrintMenu();

                var answer = (Console.ReadLine() ?? "").Trim();

                switch (answer)
                {
                    case "q":
                        return;
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

        private static int? ParseDay(string typeName)
        {
            var digits = new string(typeName.SkipWhile(c => !char.IsDigit(c)).ToArray());

            return int.TryParse(digits, out int day) ? day : null;
        }
        private void PrintMenu()
        {
            WriteFormatted(ConsoleColor.Black, ConsoleColor.Yellow, "\r\nEnter puzzle (e.g. ", false);
            WriteFormatted(ConsoleColor.Black, ConsoleColor.Green, "2a", false);
            WriteFormatted(ConsoleColor.Black, ConsoleColor.Yellow, " for Day 2 part 1).");
            WriteFormatted(ConsoleColor.Black, ConsoleColor.Yellow, "Available: " + string.Join(", ", _menu.Keys) + "\r\nOr enter q to quit.");
        }
    }
}
