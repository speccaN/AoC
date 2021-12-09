using AoCRunner.Common;
using System.Text;
using ML = MoreLinq.Extensions.PermutationsExtension;

namespace AoCRunner._2021
{
    internal class Day08 : BaseDay
    {
        public override int Year => 2021;

        protected override void Run(byte[] input)
        {
            if (input == null) return;

            var raw_data = input.GetString().Trim().Split("\n");
            var data = raw_data.Select(line => line.Split(" | ")[1].Split(" "));
            var good = new[] { 2, 4, 3, 7 };
            var ans = 0;

            foreach (var output in data)
            foreach (var digit in output)
                if (good.Contains(digit.Length))
                    ans += 1;
            PartA = $"{ans}";

            ans = 0;
            var digitKeys = new[]{
                "abcefg",
                "cf",
                "acdeg",
                "acdfg",
                "bcdf",
                "abdfg",
                "abdefg",
                "acf",
                "abcdefg",
                "abcdfg"
            };
            var digits = digitKeys.OrderBy(o => o).ToList();
            var abc = "abcdefg";
            var perms = ML.Permutations(abc).Select(s => new string(s.ToArray()));
            foreach (var line in raw_data.Select(s => s.Split(" | ")).ToArray())
            {
                var clues = line[0].Split(" ").OrderBy(o => o).ToArray();
                var nums = line[1].Split(" ").ToArray();

                // Loop and do Substitution for all permutations until match is found,
                // ex. a->b | b->f | d->a etc...
                foreach (var perm in perms) 
                {
                    var key = abc.Zip(perm).ToDictionary(k => k.First, v => v.Second);
                    var newClues = clues.Select(
                            (clue, i) => 
                                new string(clue.Select(c => key[c]).OrderBy(o => o).ToArray()))
                        .OrderBy(o => o)
                        .ToList();

                    if (newClues.SequenceEqual(digits))
                    {
                        ans += nums.Select((s, i) => new string(s.Select(c => key[c]).OrderBy(o => o).ToArray()))
                            .Aggregate("", (str, digit) => str += Array.IndexOf(digitKeys, digit), res => int.Parse(res));
                        break;
                    }
                }
            }
            PartB = $"{ans}";
        }
    }
}
