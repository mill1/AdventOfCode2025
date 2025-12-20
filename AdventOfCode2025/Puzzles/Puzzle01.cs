using AdventOfCode2025.Puzzles.Interfaces;

namespace AdventOfCode2025.Puzzles
{
    public class Puzzle01 : IPuzzle
    {
        public void Part1(bool useExample)
        {
            // Read all rotation instructions from inputDay1.txt (or replace as needed)
            var lines = useExample ? GetExampleData() : File.ReadAllLines(this.GetPathInputFile()).ToList();

            int position = 50;
            int zeroCount = 0;

            foreach (var line in lines)
            {
                char dir = line[0];
                int distance = int.Parse(line.Substring(1));

                if (dir == 'L')
                {
                    position = (position - distance) % 100;

                    if (position < 0)
                        position += 100;
                }
                else // 'R'
                {
                    position = (position + distance) % 100;
                }

                if (position == 0)
                    zeroCount++;
            }

            Console.WriteLine($"Count: {zeroCount}");
        }

        public void Part2(bool useExample)
        {
            var lines = useExample ? GetExampleData() : File.ReadAllLines(this.GetPathInputFile()).ToList();

            int position = 50;
            int zeroCount = 0;

            foreach (var line in lines)
            {
                char dir = line[0];
                int distance = int.Parse(line.Substring(1));

                for (int i = 0; i < distance; i++)
                {
                    if (dir == 'R')
                        position = (position + 1) % 100;
                    else // 'L'
                        position = (position - 1 + 100) % 100;

                    if (position == 0)
                        zeroCount++;
                }
            }

            Console.WriteLine($"Count: {zeroCount}");
        }

        private static List<string> GetExampleData()
        {
            return new List<string>
            {
                "L68",
                "L30",
                "R48",
                "L5",
                "R60",
                "L55",
                "L1",
                "L99",
                "R14",
                "L82"
            };
        }
    }
}
