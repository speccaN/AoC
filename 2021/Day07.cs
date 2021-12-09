using AoCRunner.Common;

namespace AoCRunner._2021
{
    internal class Day07 : BaseDay
    {
        public override int Year => 2021;

        protected override void Run(byte[] input)
        {
            if (input == null) return;

            var inp = input.GetIntArray();
            var median = inp.OrderBy(o => o).ToArray()[inp.Length/2];
            var partOne = inp.Aggregate(0, (acc, curr) => acc + Math.Abs(curr - median));

            // Part 2
            var triangular = (int n) => n * (n + 1) / 2;
            var nums = Enumerable.Range(0, inp.Max() + 1);
            var partTwo = nums.Aggregate(int.MaxValue, (best, candidate) =>
            {
                var fuel = inp.Aggregate(0, (acc, curr) => acc += triangular(Math.Abs(curr - candidate)));
                return Math.Min(fuel, best);
            });

            PartA = $"{partOne}";
            PartB = $"{partTwo}";
        }
    }
}
