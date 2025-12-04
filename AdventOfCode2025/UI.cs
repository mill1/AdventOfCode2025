using AdventOfCode2025.Puzzles;

namespace AdventOfCode2025
{
    public class UI
    {
        public void Run()
        {
            bool quit = false;

            while (!quit)
            {
                Action<bool> puzzleTarget = InvalidOption;

                Console.WriteLine("Which puzzle? (Day 2 Part 1 = 2a, q to quit)");
                var answer = Console.ReadLine();

                switch (answer)
                {
                    case "1a": puzzleTarget = new Puzzle1().Part1; break;
                    case "1b": puzzleTarget = new Puzzle1().Part2; break;
                    case "2a": puzzleTarget = new Puzzle2().Part1; break;
                    case "2b": puzzleTarget = new Puzzle2().Part2; break;
                    case "3a": puzzleTarget = new Puzzle3().Part1; break;
                    case "3b": puzzleTarget = new Puzzle3().Part2; break;
                    case "4a": puzzleTarget = new Puzzle4().Part1; break;
                    case "4b": puzzleTarget = new Puzzle4().Part2; break;

                    case "q": quit = true; continue;

                    default:
                         break;
                }

                Console.WriteLine("Example data? (y/n)");
                bool example = Console.ReadLine() == "y";

                puzzleTarget(example);
            }
        }

        public void InvalidOption(bool dummy)
        {
            Console.BackgroundColor = ConsoleColor.DarkBlue;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Invalid option");
            Console.ResetColor();
        }

        private record MenuItem(string Label, string Key, Action Action);
    }
}
