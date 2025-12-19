using AdventOfCode2025.Puzzles.Interfaces;

namespace AdventOfCode2025.Puzzles
{
    public class Puzzle9 : IPuzzle
    {
        public void Part1(bool useExample)
        {
            var gridLocations = ParseGridLocations(useExample);

            var maxArea = 0L;

            foreach (var loc1 in gridLocations)
            {
                foreach (var loc2 in gridLocations)
                {
                    if (loc1 == loc2)
                        continue;

                    if (loc2.Row < loc1.Row || loc2.Col < loc1.Col)
                        continue;

                    var dRow = loc2.Row - loc1.Row + 1;
                    var dCol = loc2.Col - loc1.Col + 1;

                    var area = dRow * dCol;

                    if (area > maxArea)
                        maxArea = area;
                }
            }


            Console.WriteLine($"Max: {maxArea}");
        }

        public void Part2(bool useExample)
        {
            throw new NotImplementedException();
        }

        private List<(long Row, long Col)> ParseGridLocations(bool useExample) // Y, X
        {
            var lines = useExample ? GetExampleData() : File.ReadAllLines(this.GetPathInputFile()).ToList();

            return lines
                .Select(line =>
                {
                    var c = line.Split(',').Select(long.Parse).ToArray();
                    return (c[1], c[0]);
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
