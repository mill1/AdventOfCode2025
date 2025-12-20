using AdventOfCode2025.Puzzles.Interfaces;

namespace AdventOfCode2025.Puzzles
{
    internal class Puzzle02 : IPuzzle
    {
        public void Part1(bool useExample)
        {
            {
                string input = useExample ? GetExampleData() : File.ReadAllText(this.GetPathInputFile()).Trim();

                long total = 0;

                foreach (var range in input.Split(','))
                {
                    var parts = range.Split('-');
                    long start = long.Parse(parts[0]);
                    long end = long.Parse(parts[1]);

                    for (long id = start; id <= end; id++)
                    {
                        if (IsRepeatedPattern(id.ToString()))
                            total += id;
                    }
                }

                Console.WriteLine($"Total: {total}");
            }

            static bool IsRepeatedPattern(string s)
            {
                // Must be even length to be A repeated twice
                if (s.Length % 2 != 0)
                    return false;

                string half = s.Substring(0, s.Length / 2);
                return half + half == s;
            }
        }

        public void Part2(bool useExample)
        {
            string input = useExample ? GetExampleData() : File.ReadAllText(this.GetPathInputFile()).Trim();

            long total = 0;

            foreach (var range in input.Split(','))
            {
                var parts = range.Split('-');
                long start = long.Parse(parts[0]);
                long end = long.Parse(parts[1]);

                for (long id = start; id <= end; id++)
                {
                    if (IsRepeatedPattern(id.ToString()))
                        total += id;
                }
            }

            Console.WriteLine($"Total: {total}");
        }

        private static string GetExampleData()
        {
            return "11-22,95-115,998-1012,1188511880-1188511890," +
                "222220-222224,1698522-1698528,446443-446449," +
                "38593856-38593862,565653-565659,824824821-824824827," +
                "2121212118-2121212124";
        }

        static bool IsRepeatedPattern(string s)
        {
            int n = s.Length;

            // Try all possible block sizes
            for (int block = 1; block <= n / 2; block++)
            {
                if (n % block != 0)
                    continue;

                string piece = s.Substring(0, block);
                int repeats = n / block;

                bool ok = true;
                for (int r = 1; r < repeats; r++)
                {
                    if (s.Substring(r * block, block) != piece)
                    {
                        ok = false;
                        break;
                    }
                }

                if (ok) 
                    return true;
            }

            return false;
        }
    }
}
