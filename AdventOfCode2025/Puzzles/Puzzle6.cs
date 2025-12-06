using AdventOfCode2025.Puzzles.Interfaces;
using System.Text;

namespace AdventOfCode2025.Puzzles
{
    public class Puzzle6 : IPuzzle
    {
        public void Part1(bool useExample)
        {
            var lines = useExample ? GetExampleData() : File.ReadAllLines(@"Puzzles\Input\InputPuzzle6.txt").ToList();
            var problems = new List<List<string>>();

            for (int i = 0; i < lines.Count; i++)
                problems.Add(SingularizeSpaces(lines[i].Trim()).Split(' ').ToList());

            long grandTotal = 0;

            for (int i = 0; i < problems[1].Count; i++)
            {
                long total = 0;

                for (int j = 0; j < lines.Count - 1; j++)
                {
                    var value = long.Parse(problems[j][i]);

                    switch (problems.Last()[i])
                    {
                        case "+":
                            total += value;
                            break;

                        case "*":
                            if (j == 0)
                                total = value;
                            else
                                total *= value;
                            break;

                        default:
                            throw new NotImplementedException();
                    }
                }
                grandTotal += total;
            }

            Console.WriteLine($"Grand total: {grandTotal}");
        }

        public void Part2(bool useExample)
        {
            throw new NotImplementedException();
        }

        private string SingularizeSpaces(string line)
        {
            while (line.Contains("  "))
                line = line.Replace("  ", " ");

            return line;
        }

        private static List<string> GetExampleData()
        {
            return new List<string>
            {
                "123 328  51 64 ",
                " 45 64  387 23 ",
                " 6 98  215 314",
                "*   +   *   +  "
            };
        }
    }
}
