using AdventOfCode2025.Puzzles.Interfaces;

namespace AdventOfCode2025.Puzzles
{
    public class Puzzle08 : IPuzzle
    {
        public void Part1(bool useExample) 
            => Console.WriteLine($"Total: {Solve(useExample, true)}");

        public void Part2(bool useExample)
            => Console.WriteLine($"Total: {Solve(useExample, false)}");

        private long Solve(bool useExample, bool part1)
        {
            var junctions = ParseJunctions(useExample);
            var connections = BuildConnections(junctions);            

            var (productPart1, productPart2) = ProcessConnections(junctions, connections, useExample ? 10 : 1000);

            return part1 ? productPart1 : productPart2;
        }

        private List<(int X, int Y, int Z)> ParseJunctions(bool useExample)
        {
            var lines = useExample ? GetExampleData() : File.ReadAllLines(this.GetPathInputFile()).ToList();

            return lines
                .Select(line =>
                {
                    var c = line.Split(',').Select(int.Parse).ToArray();
                    return (c[0], c[1], c[2]);
                }).ToList();
        }

        private List<(long Distance, int Left, int Right)>BuildConnections(List<(int X, int Y, int Z)> junctions)
        {
            List<(long Distance, int Left, int Right)> connections = [];

            for (int i = 0; i < junctions.Count; i++)
                for (int j = i + 1; j < junctions.Count; j++)
                    connections.Add((SquaredDistance(junctions[i], junctions[j]), i,j));

            connections.Sort((a, b) => a.Distance.CompareTo(b.Distance));
            return connections;
        }

        private (long part1, long part2) ProcessConnections(
            List<(int X, int Y, int Z)> junctions,
            List<(long Distance, int Left, int Right)> connections,
            int maxConnections)
        {
            var state = new CircuitState(junctions.Count);

            long part1 = 0;
            long part2 = 0;

            for (int i = 0; i < connections.Count; i++)
            {
                var (_, left, right) = connections[i];

                if (ProcessConnection(state, (left, right)))
                {
                    if (state.CircuitCount == 1)
                    {
                        part2 = (long)junctions[left].X * junctions[right].X;
                        break;
                    }
                }

                if (i + 1 == maxConnections)
                    part1 = GetProductPart1(state.Size);
            }

            return (part1, part2);
        }

        private bool ProcessConnection(CircuitState state, (int Left, int Right) edge)
        {
            if (!MergeCircuits(edge.Left, edge.Right, state.Circuit, state.Size))
                return false;

            state.CircuitCount--;
            return true;
        }

        private static long SquaredDistance((int X, int Y, int Z) a, (int X, int Y, int Z) b)
        {
            long dx = a.X - b.X;
            long dy = a.Y - b.Y;
            long dz = a.Z - b.Z;

            return (dx * dx) + (dy * dy) + (dz * dz);
        }

        private bool MergeCircuits(int a, int b, int[] circuit, int[] size)
        {
            int ca = circuit[a];
            int cb = circuit[b];

            if (ca == cb)
                return false;

            for (int i = 0; i < circuit.Length; i++)
            {
                if (circuit[i] == cb)
                    circuit[i] = ca;
            }

            size[ca] += size[cb];
            size[cb] = 0;

            return true;
        }

        private long GetProductPart1(int[] sizes)
        {
            var topThree = sizes
                .Where(s => s > 0)
                .OrderByDescending(s => s)
                .Take(3)
                .ToArray();

            return (long)topThree[0] * topThree[1] * topThree[2];
        }

        private static List<string> GetExampleData()
        {
            return new List<string>
            {
                "162,817,812",
                "57,618,57",
                "906,360,560",
                "592,479,940",
                "352,342,300",
                "466,668,158",
                "542,29,236",
                "431,825,988",
                "739,650,466",
                "52,470,668",
                "216,146,977",
                "819,987,18",
                "117,168,530",
                "805,96,715",
                "346,949,466",
                "970,615,88",
                "941,993,340",
                "862,61,35",
                "984,92,344",
                "425,690,689",
            };
        }

        private class CircuitState
        {
            public int[] Circuit { get; }
            public int[] Size { get; }
            public int CircuitCount { get; set; }

            public CircuitState(int count)
            {
                Circuit = new int[count];
                Size = new int[count];
                CircuitCount = count;

                for (int i = 0; i < count; i++)
                {
                    Circuit[i] = i;
                    Size[i] = 1;
                }
            }
        }
    }
}
