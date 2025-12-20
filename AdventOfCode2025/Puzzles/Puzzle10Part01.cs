using AdventOfCode2025.Puzzles.Interfaces;

namespace AdventOfCode2025.Puzzles
{
    public partial class Puzzle10 : IPuzzle
    {
        // I cannot take credit for this solution; borrowed from https://github.com/sebastianlay/AdventOfCode2025

        public void Part1(bool useExample)
        {
            var lines = useExample ? GetExampleData() : File.ReadAllLines(this.GetPathInputFile());
            var machines = lines.Select(ParseToMachinePart1);

            int min = 0;

            foreach (var machine in machines)
            {
                for (int presses = 1; ; presses++)
                {
                    var seed = new int[machine.Buttons.Length];

                    if (GetCombinations(0, presses, seed).Any(c => Applies(machine, c)))
                    {
                        min += presses;
                        break;
                    }
                }
            }

            Console.WriteLine($"Min: {min}");
        }

        private static bool Applies(MachinePart1 machine, int[] combination)
        {
            var lights = new bool[machine.Lights.Length];

            for (int i = 0; i < combination.Length; i++)
            {
                if ((combination[i] & 1) == 0)
                    continue;

                foreach (var index in machine.Buttons[i])
                    lights[index] = !lights[index];
            }

            return lights.SequenceEqual(machine.Lights);
        }

        private static IEnumerable<int[]> GetCombinations(int index, int remaining, int[] values)
        {
            if (index == values.Length - 1)
            {
                values[index] = remaining;
                yield return values;
                yield break;
            }

            for (var take = 0; take <= remaining; take++)
            {
                values[index] = take;
                var combinations = GetCombinations(index + 1, remaining - take, values);
                foreach (var combination in combinations)
                    yield return combination;
            }
        }

        private static MachinePart1 ParseToMachinePart1(string line)
        {
            var lights = line[1..line.IndexOf(']')]
                .Select(c => c == '#')
                .ToArray();

            var buttons = line[(line.IndexOf(']') + 2)..(line.IndexOf('{') - 1)]
                .Split(' ')
                .Select(b => b[1..^1].Split(','))
                .Select(b => b.Select(int.Parse).ToArray())
                .ToArray();

            return new MachinePart1(lights, buttons);
        }

        internal record MachinePart1(bool[] Lights, int[][] Buttons);

        private string[] GetExampleData()
        {
            return new[]
            {
                "[.##.] (3) (1,3) (2) (2,3) (0,2) (0,1) {3,5,4,7}",
                "[...#.] (0,2,3,4) (2,3) (0,4) (0,1,2) (1,2,3,4) {7,5,12,7,2}",
                "[.###.#] (0,1,2,3,4) (0,3,4) (0,1,2,4,5) (1,2) {10,11,11,5,10,5}"
            };
        }
    }
}
