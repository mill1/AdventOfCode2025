using AdventOfCode2025.Puzzles.Interfaces;

namespace AdventOfCode2025.Puzzles
{
    public class Puzzle6 : IPuzzle
    {
        public void Part1(bool useExample)
        {
            var lines = useExample ? GetExampleData() : File.ReadAllLines(@"Puzzles\Input\InputPuzzle6.txt").ToList();

            var solution = 0;

            Console.WriteLine($"Solution: {solution}");
        }

        public void Part2(bool useExample)
        {
            throw new NotImplementedException();
        }

        private static List<string> GetExampleData()
        {
            return new List<string>
            {
                "",
            };
        }
    }
}
