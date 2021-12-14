using AoCRunner.Common;

namespace AoCRunner._2021
{
    internal class Day14 : BaseDay
    {
        public override int Year => 2021;

        protected override void Run(byte[] input)
        {
            if (input == null) return;

            var raw_input = input.GetLines();
            var t = raw_input[0];
            var formulas = raw_input[1..].Select(line => line.Split(" -> ").ToArray()).ToDictionary(k => k[0], v => v[1]);

            var pairs = new Dictionary<string, long>();
            for (int i = 0; i < t.Length-1; i++)
            {
                var pair = new string(new[] { t[i], t[i + 1] });
                if (pairs.TryGetValue(pair, out var fnd))
                    pairs[pair] += 1;
                else
                    pairs[pair] = 1;
            }

            var indices = new[] { 10, 40 };
            for (int i = 0; i <= 40; i++)
            {
                if (indices.Contains(i))
                {
                    var chrs = new Dictionary<char, long>();
                    foreach (var (k, v) in pairs)
                        _ = chrs.TryGetValue(k[0], out var cnt) ? chrs[k[0]] = cnt + v : chrs[k[0]] = v;
                    chrs[t[^1]] += 1;

                    Console.WriteLine(chrs.Max(m => m.Value)- chrs.Min(m => m.Value));
                }

                var c2 = new Dictionary<string, long>();
                foreach (var (k,v) in pairs)
                {
                    var n1 = k[0] + formulas[k];
                    var n2 = formulas[k] + k[1];
                    _ = c2.TryGetValue(n1, out var c2f) ? c2[n1] = c2f + v : c2[n1] = v;
                    _ = c2.TryGetValue(n2, out     c2f) ? c2[n2] = c2f + v : c2[n2] = v;
                }
                pairs = c2;
            }
        }
    }
}
