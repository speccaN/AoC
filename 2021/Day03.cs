using AoCRunner.Common;
using System.Text;

namespace AoCRunner._2021
{
    internal class Day03 : BaseDay
    {
        public override int Year => 2021;

        public override int DayNumber => 3;

        long PartOne(string[] parsedInput)
        {
            var gammaArr = new int[parsedInput[0].Length];
            for (int i = 0; i < parsedInput.Length; i++)
            {
                for (int y = 0; y < parsedInput[i].Length; y++)
                {
                    gammaArr[y] = parsedInput[i][y] == '1' ? gammaArr[y]+1 : gammaArr[y];
                }
            }
            var gamma = gammaArr.Aggregate(new StringBuilder(), (sb, next) => sb.Append(next > parsedInput.Length / 2 ? '1' : '0')).ToString();
            var epsilon = gamma.Aggregate(new StringBuilder(), (sb, next) => sb.Append(next == '1' ? '0' : '1')).ToString();

            return Convert.ToInt32(gamma,2) * Convert.ToInt32(epsilon,2);
        }

        int PartTwo(string[] parsedInput) 
        {
            var x = Recur(parsedInput, 0);
            var y = RecurCO(parsedInput, 0);
            return Convert.ToInt32(x[0], 2) * Convert.ToInt32(y[0], 2);
        }

        string[] Recur(string[] input, int index)
        {
            if (input.Length == 1) return input;

            var zeroes = new List<string>();
            var ones = new List<string>();

            for (int i = 0; i < input.Length; i++)
            {
                if (input[i][index] == '1')
                    ones.Add(input[i]);
                else
                    zeroes.Add(input[i]);
            }
            if (ones.Count >= zeroes.Count) return Recur(ones.ToArray(), index + 1);
            
            return Recur(zeroes.ToArray(), index + 1);
        }

        string[] RecurCO(string[] input, int index)
        {
            if (input.Length == 1) return input;

            var zeroes = new List<string>();
            var ones = new List<string>();

            for (int i = 0; i < input.Length; i++)
            {
                if (input[i][index] == '1')
                    ones.Add(input[i]);
                else
                    zeroes.Add(input[i]);
            }
            if (ones.Count >= zeroes.Count) return RecurCO(zeroes.ToArray(), index + 1);

            return RecurCO(ones.ToArray(), index + 1);
        }

        protected override void Run(byte[] input)
        {
            if (input == null) return;

            var parsedInput = input.GetLines();

            PartA = $"{PartOne(parsedInput)}";
            PartB = $"{PartTwo(parsedInput)}";
        }
    }
}
