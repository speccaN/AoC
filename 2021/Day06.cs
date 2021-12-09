using AoCRunner.Common;
using System.Text;

namespace AoCRunner._2021
{
    internal class Day06 : BaseDay
    {
        public override int Year => 2021;

        long Run(int[] input, int days)
        {
            var timers = new long[9];

            for (int i = 0; i < input.Length; i++)
                timers[input[i]]++;

            for (int i = 0; i < days; i++)
                Update(timers);

            return timers.Sum();
        }

        void Update(long[] timers)
        {
            var amountOfNewFish = timers[0];

            for (int i = 0; i < 8; i++)
                timers[i] = timers[i + 1];

            timers[8] = amountOfNewFish;
            timers[6] += amountOfNewFish;
        }

        protected override void Run(byte[] input)
        {
            if (input == null) return;

            var nums = input.GetString().Split(",").Select(int.Parse).ToArray();

            PartA = $"{Run(nums, 80)}";
            PartB = $"{Run(nums, 256)}";
        }
    }
}
