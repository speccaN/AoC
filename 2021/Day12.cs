using AoCRunner.Common;

namespace AoCRunner._2021
{
    internal class Day12 : BaseDay
    {
        public override int Year => 2021;
        Dictionary<string, HashSet<string>> paths = new();
        protected override void Run(byte[] input)
        {
            if (input == null) return;

            foreach (var line in input.GetLines())
            {
                var split = line.Split("-");
                var a = split[0];
                var b = split[1];
                if (paths.TryGetValue(a, out var path))
                    path.Add(b);
                else
                    paths.Add(a, new() { b });
                if (paths.TryGetValue(b, out path))
                    path.Add(a);
                else
                    paths[b] = new() { a };
                paths[b].Append(a);
            }

            PartA = $"{Solve(true)}";
            PartB = $"{Solve(false)}";
        }

        int Solve(bool partOne)
        {
            var Q = new Queue<(string, HashSet<string>, bool twice)>();
            Q.Enqueue(("start", new HashSet<string> { "start" }, false));
            var ans = 0;
            while (Q.TryDequeue(out var node))
            {
                var (pos, small, twice) = node;
                if (pos == "end")
                {
                    ans++;
                    continue;
                }
                foreach (var y in paths[pos])
                {
                    if (small.Contains(y) == false)
                    {
                        var new_small = new HashSet<string>(small);
                        if (y.All(char.IsLower))
                            new_small.Add(y);
                        Q.Enqueue((y, new_small, twice));
                    }
                    else if (small.Contains(y) && !twice && new[] { "start", "end" }.Contains(y) == false && !partOne)
                    {
                        Q.Enqueue((y, small, true));
                    }
                }
            }

            return ans;
        }
    }
}
