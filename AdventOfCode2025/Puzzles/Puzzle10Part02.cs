using AdventOfCode2025.Puzzles.Interfaces;
using System.Numerics;

namespace AdventOfCode2025.Puzzles
{
    public partial class Puzzle10 : IPuzzle
    {
        // I cannot take credit for this solution; borrowed from https://github.com/sebastianlay/AdventOfCode2025

        public void Part2(bool useExample)
        {
            var lines = useExample ? GetExampleData() : File.ReadAllLines(this.GetPathInputFile());
            var machines = lines.Select(ParseToMachinePart2);

            long min = 0L;

            foreach (var machine in machines)
            {
                var combiLookupValues = BuildCombinationLookup(machine);
                min += ResolvePresses(machine.Counters, combiLookupValues); ;
            }

            Console.WriteLine($"Min: {min}");
        }

        private static int ResolvePresses(int[] joltage, CombinationLookup combiLookupValues)
        {
            var result = 10_000;

            if (joltage.All(j => j == 0))
                return 0;

            if (joltage.Any(j => j < 0))
                return result;

            var parity = string.Join(',', joltage.Select(j => j % 2 == 0));

            if (!combiLookupValues.Parities.TryGetValue(parity, out var combinations))
                return result;

            foreach (var combination in combinations)
            {
                if (result == 0)
                    break;

                result = EvaluateCombination(joltage, combiLookupValues, result, combination);
            }


            return result;
        }

        private static int EvaluateCombination(int[] joltage, CombinationLookup combiLookup, int result, uint combination)
        {
            var lookedUpJoltage = combiLookup.Joltages[combination];
            var newJoltage = new int[joltage.Length];

            for (var i = 0; i < joltage.Length; i++)
                newJoltage[i] = (joltage[i] - lookedUpJoltage[i]) / 2;

            var combinationPresses = BitOperations.PopCount(combination);
            var presses = ResolvePresses(newJoltage, combiLookup);
            var combinationResult = combinationPresses + 2 * presses;

            result = Math.Min(combinationResult, result);

            return result;
        }

        private static CombinationLookup BuildCombinationLookup(Machine machine)
        {
            var parities = new Dictionary<string, List<uint>>();
            var joltages = new Dictionary<uint, int[]>();

            int buttons = machine.Buttons.Length;
            uint combinations = (uint)(1 << buttons);

            for (uint mask = 0; mask < combinations; mask++)
            {
                var joltage = new int[machine.Counters.Length];

                for (int b = 0; b < buttons; b++)
                {
                    if ((mask & (1u << b)) == 0)
                        continue;

                    foreach (var idx in machine.Buttons[b])
                        joltage[idx]++;
                }

                var key = GetParityKey(joltage);

                parities.TryAdd(key, new List<uint>());
                parities[key].Add(mask);

                joltages[mask] = joltage;
            }

            return new CombinationLookup(parities, joltages);
        }

        private static string GetParityKey(int[] values)
            => string.Join(',', values.Select(v => (v & 1) == 0));


        private static Machine ParseToMachinePart2(string line)
        {
            var buttons = line[(line.IndexOf(']') + 2)..(line.IndexOf('{') - 1)]
                .Split(' ')
                .Select(b => b[1..^1].Split(','))
                .Select(b => b.Select(int.Parse).ToArray())
                .ToArray();

            var counters = line[(line.IndexOf('{') + 1)..^1]
                .Split(',')
                .Select(int.Parse)
                .ToArray();

            return new Machine(buttons, counters);
        }

        internal sealed record Machine(int[][] Buttons, int[] Counters);

        internal sealed record CombinationLookup(Dictionary<string, List<uint>> Parities, Dictionary<uint, int[]> Joltages);
    }
}
