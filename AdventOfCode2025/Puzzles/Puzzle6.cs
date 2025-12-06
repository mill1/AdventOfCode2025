using AdventOfCode2025.Puzzles.Interfaces;

namespace AdventOfCode2025.Puzzles
{
    public class Puzzle6 : IPuzzle
    {
        public void Part1(bool useExample)
        {
            var lines = useExample ? GetExampleData() : File.ReadAllLines(@"Puzzles\Input\InputPuzzle6.txt").ToList();
            List<List<string>> problems = GetProblems(lines);

            long grandTotal = 0;

            for (int i = 0; i < problems[0].Count; i++)
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
            var lines = useExample ? GetExampleData() : File.ReadAllLines(@"Puzzles\Input\InputPuzzle6.txt").ToList();
            var problemData = lines.Take(lines.Count - 1).ToList();
            var problems = ParseColumnProblems(problemData);

            var operations = SingularizeSpaces(lines.Last().Trim()).Split(' ').ToList();

            long grandTotal = 0;

            for (int i = 0; i < problems.Count; i++)
            {
                long total = 0;

                for (int j = 0; j < problems[i].Count; j++)
                {
                    var value = problems[i][j];

                    switch (operations[i])
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

        public static List<List<long>> ParseColumnProblems(List<string> lines)
        {
            int rows = lines.Count;
            int cols = lines[0].Length;
            var results = new List<List<long>>();
            int col = 0;

            while (col < cols)
            {
                // Skip problem separators
                bool isSeparator = true;
                for (int row = 0; row < rows; row++)
                {
                    if (lines[row][col] != ' ')
                    {
                        isSeparator = false;
                        break;
                    }
                }

                if (isSeparator)
                {
                    col++;
                    continue;
                }

                // This is a problem column; collect consecutive non-separator columns
                var problemCols = new List<int>();
                while (col < cols)
                {
                    bool empty = true;
                    for (int row = 0; row < rows; row++)
                    {
                        if (lines[row][col] != ' ')
                        {
                            empty = false;
                            break;
                        }
                    }
                    if (empty) 
                        break;

                    problemCols.Add(col);
                    col++;
                }

                // Parse each number vertically
                var numbers = new List<long>();
                foreach (int c in problemCols)
                {
                    string num = "";
                    for (int r = 0; r < rows; r++)
                    {
                        num += lines[r][c];
                    }
                    numbers.Add(long.Parse(num));
                }
                results.Add(numbers);
            }

            return results;
        }

        private List<List<string>> GetProblems(List<string> lines)
        {
            var problems = new List<List<string>>();

            for (int i = 0; i < lines.Count; i++)
                problems.Add(SingularizeSpaces(lines[i].Trim()).Split(' ').ToList());

            return problems;
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
                "  6 98  215 314",
                "*   +   *   +  "
            };
        }
    }
}
