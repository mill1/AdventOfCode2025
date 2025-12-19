namespace AdventOfCode2025
{
    public static class ObjectExtensions
    {
        public static string GetPathInputFile(this object obj) 
        {
            return @$"Puzzles\Input\Input{obj.GetType().Name}.txt";
        }
    }
}
