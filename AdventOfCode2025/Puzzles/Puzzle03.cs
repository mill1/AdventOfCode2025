using AdventOfCode2025.Puzzles.Interfaces;

namespace AdventOfCode2025.Puzzles
{
    public class Puzzle03 : IPuzzle
    {
        public void Part1(bool useExample)
        {
            var lines = useExample ? GetExampleData() : File.ReadAllLines(this.GetPathInputFile()).ToList();

            int sum = 0;

            foreach (var item in lines)
            {
                int highest = 0;
                int indexOfHighest = -1;

                // first digit
                for (int i = 0; i < item.Length - 1; i++)
                {
                    if (item[i] - '0' > highest)
                    {
                        highest = item[i] - '0';
                        indexOfHighest = i;
                    }
                }

                // second digit
                highest = 0;
                int indexOfSecondHighest = -1;

                for (int i = indexOfHighest + 1; i < item.Length; i++)
                {
                    if (item[i] - '0' > highest)
                    {
                        highest = item[i] - '0';
                        indexOfSecondHighest = i;
                    }
                }

                sum += int.Parse($"{item[indexOfHighest]}{item[indexOfSecondHighest]}");
            }

            Console.WriteLine($"Sum: {sum}");
        }

        public void Part2(bool useExample)
        {
            var lines = useExample ? GetExampleData() : File.ReadAllLines(this.GetPathInputFile()).ToList();

            long sum = 0;

            foreach (var item in lines)
            {
                const int keep = 12; // number of digits to keep
                int toRemove = item.Length - keep;
                var stack = new List<char>();

                foreach (char c in item)
                {
                    while (stack.Count > 0 && toRemove > 0 && stack[stack.Count - 1] < c)
                    {
                        stack.RemoveAt(stack.Count - 1);
                        toRemove--;
                    }
                    stack.Add(c);
                }

                // Only keep the first 12 digits
                var valueStr = new string(stack.Take(keep).ToArray());
                long value = long.Parse(valueStr);
                sum += value;
            }

            Console.WriteLine($"Sum: {sum}");
        }

        private static List<string> GetExampleData()
        {
            return new List<string>
            {
                "987654321111111",
                "811111111111119",
                "234234234234278",
                "818181911112111"
            };
        }
    }
}
