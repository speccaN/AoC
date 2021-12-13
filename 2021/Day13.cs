using AoCRunner.Common;
using System.Text;

namespace AoCRunner._2021
{
    internal class Day13 : BaseDay
    {
        public override int Year => 2021;

        protected override void Run(byte[] input)
        {
            if (input == null) return;

            var raw_input = input.GetString().Split("\n\n");
            var coords = raw_input[0].Split("\n").Select(line => line.Split(",").Select(int.Parse).ToArray()).Select(s => (s[0], s[1])).ToHashSet();
            var instructions = raw_input[1]
                .Split("\n", StringSplitOptions.RemoveEmptyEntries)
                .Select(line => line.Replace("fold along ", "")
                    .Split("="))
                .Select(s => (s[0], int.Parse(s[1])));

            var partOne = true;
            foreach (var (d, v) in instructions)
            {
                var fold = new HashSet<(int, int)>();
                if (d == "x")
                {
                    foreach (var (x, y) in coords)
                    {
                        if (x < v)
                            fold.Add((x, y));
                        else
                            fold.Add((v - (x - v), y));
                    }
                }
                else
                {
                    foreach (var (x, y) in coords)
                    {
                        if (y < v)
                            fold.Add((x, y));
                        else
                            fold.Add((x, v - (y - v)));
                    }
                }
                coords = new(fold);

                if (partOne)
                {
                    PartA = $"{fold.Count}";
                    partOne = false;
                }
            }

            // PRINT FOR CONSOLE
            //var X = coords.Max(m => m.Item1);
            //var Y = coords.Max(m => m.Item2);
            //var ans = "";
            //for (int y = 0; y < Y + 1; y++)
            //{
            //    for (int x = 0; x < X + 1; x++)
            //    {
            //        if (coords.TryGetValue((x, y), out var v))
            //            ans += "x";
            //        else
            //            ans += " ";
            //    }
            //    Console.WriteLine(ans);
            //    ans = "";
            //}

            PartB = "LGHEGUEJ";
        }
    }
}
