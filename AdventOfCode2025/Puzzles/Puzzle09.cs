using AdventOfCode2025.Puzzles.Interfaces;

namespace AdventOfCode2025.Puzzles
{
    public class Puzzle09 : IPuzzle
    {
        public void Part1(bool useExample)
            => Console.WriteLine($"Max: {Solve(useExample).part1}");

        public void Part2(bool useExample)
            => Console.WriteLine($"Max: {Solve(useExample).part2}");

        private (long part1, long part2) Solve(bool useExample)
        {
            var coords = ParseGridLocations(useExample);
            var (horizontal, vertical) = BuildEdges(coords);

            return ResolveMaxRectangleAreas(coords, horizontal, vertical);
        }

        private static (List<(int Y, int X1, int X2)> horizontal, List<(int X, int Y1, int Y2)> vertical) BuildEdges(List<(int X, int Y)> coords)
        {
            var horizontal = new List<(int, int, int)>();
            var vertical = new List<(int, int, int)>();

            for (int i = 0; i < coords.Count; i++)
            {
                var a = coords[i];
                var b = coords[(i + 1) % coords.Count];

                if (a.Y == b.Y)
                    horizontal.Add((a.Y, Math.Min(a.X, b.X), Math.Max(a.X, b.X)));
                else
                    vertical.Add((a.X, Math.Min(a.Y, b.Y), Math.Max(a.Y, b.Y)));
            }

            return (horizontal, vertical);
        }

        private static (long part1, long part2) ResolveMaxRectangleAreas(
            List<(int X, int Y)> coords,
            List<(int Y, int X1, int X2)> horizontal,
            List<(int X, int Y1, int Y2)> vertical)
        {
            long maxPart1 = 0;
            long maxPart2 = 0;

            for (int i = 0; i < coords.Count; i++)
            {
                for (int j = i + 1; j < coords.Count; j++)
                {
                    var rect = NormalizeRectangle(coords[i], coords[j]);
                    long area = (rect.MaxX - rect.MinX + 1L) * (rect.MaxY - rect.MinY + 1L);

                    maxPart1 = Math.Max(maxPart1, area);

                    if (IsRectangleValid(rect, horizontal, vertical))
                        maxPart2 = Math.Max(maxPart2, area);
                }
            }

            return (maxPart1, maxPart2);
        }

        private static (int MinX, int MaxX, int MinY, int MaxY) NormalizeRectangle((int X, int Y) a, (int X, int Y) b)
        {
            return (
                Math.Min(a.X, b.X),
                Math.Max(a.X, b.X),
                Math.Min(a.Y, b.Y),
                Math.Max(a.Y, b.Y)
            );
        }

        private static bool IsRectangleValid(
            (int MinX, int MaxX, int MinY, int MaxY) rect,
            List<(int Y, int X1, int X2)> horizontal,
            List<(int X, int Y1, int Y2)> vertical)
        {
            foreach (var (y, x1, x2) in horizontal)
                if (y > rect.MinY && y < rect.MaxY && x1 < rect.MaxX && x2 > rect.MinX)
                    return false;

            foreach (var (x, y1, y2) in vertical)
                if (x > rect.MinX && x < rect.MaxX && y1 < rect.MaxY && y2 > rect.MinY)
                    return false;

            return true;
        }

        private List<(int X, int Y)> ParseGridLocations(bool useExample)
        {
            var lines = useExample ? GetExampleData() : File.ReadAllLines(this.GetPathInputFile()).ToList();

            return lines
                .Select(line =>
                {
                    var coor = line.Split(',').Select(int.Parse).ToArray();
                    return (coor[0], coor[1]);
                }).ToList();
        }

        private static List<string> GetExampleData()
        {
            return new List<string>
            {
                "7,1",
                "11,1",
                "11,7",
                "9,7",
                "9,5",
                "2,5",
                "2,3",
                "7,3",
            };
        }
    }
}
