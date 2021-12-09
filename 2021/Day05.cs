using AoCRunner.Common;

namespace AoCRunner._2021
{
    internal class Day05 : BaseDay
    {
        public override int Year => 2021;

        protected override void Run(byte[] input)
        {
            if (input == null)
                return;

            var parsed = input.GetLines();

            PartA = $"{PartOne(parsed)}";
            PartB = $"{PartTwo(parsed)}";
        }

        int PartOne(string[] input) =>
            input.Select(Line.Parse)
            .Where(w => !w.IsDiagonal)
            .SelectMany(sm => sm.Process())
            .GroupBy(coord => coord)
            .Count(c => c.Count() > 1);

        Func<string, (int x, int y)> ParseNums = input =>
         {
             var nums = input.Split(",");
             return (x: int.Parse(nums[0]), y: int.Parse(nums[1]));
         };

        int PartTwo(string[] input) =>
            input.Select(Line.Parse)
            .SelectMany(sm => sm.Process())
            .GroupBy(coord => coord)
            .Count(c => c.Count() > 1);

        record Coordinates(int x, int y)
        {
            public static Coordinates Parse(string input)
            {
                var nums = input.Split(",");
                return new Coordinates(int.Parse(nums[0]), int.Parse(nums[1]));
            }
        }

        record Line(Coordinates start, Coordinates end)
        {
            public static Line Parse(string input)
            {
                var line = input.Split(" -> ");
                return new Line(Coordinates.Parse(line[0]), Coordinates.Parse(line[1]));
            }

            internal IEnumerable<Coordinates> Process()
            {
                var deltaX = end.x - start.x;
                var deltaY = end.y - start.y;
                var stepX = Math.Sign(deltaX);
                var stepY = Math.Sign(deltaY);
                var length = Math.Max(Math.Abs(deltaX), Math.Abs(deltaY)) + 1;

                return Enumerable.Range(0, length).Select(i => new Coordinates(start.x + (stepX * i), start.y + (stepY * i)));
            }

            public bool IsDiagonal => start.x != end.x && start.y != end.y;
        }
    }
}
