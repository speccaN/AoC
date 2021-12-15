using AoCRunner.Common;

namespace AoCRunner._2021
{
    internal class Day15 : BaseDay
    {
        public override int Year => 2021;

        protected override void Run(byte[] input)
        {
            if (input == null) return;

            var raw_data = input.GetLines();
            var g = new int[raw_data.Length, raw_data.Length];
            for (int y = 0; y < raw_data.Length; y++)
                for (int x = 0; x < raw_data[y].Length; x++)
                    g[x, y] = (int)char.GetNumericValue(raw_data[y][x]);


            PartA = $"{PartOne(g)}";
            PartB = $"{PartTwo(g)}";
        }

        int PartOne(int[,] g)
        {
            var cols = g.GetUpperBound(0) + 1;
            var rows = g.GetUpperBound(1) + 1;
            var ddirs = new[] { (0, 1), (0, -1), (1, 0), (-1, 0) };

            var cost = new Dictionary<(int, int), int>();
            var pq = new PriorityQueue<(int x, int y), int>();
            pq.Enqueue((0, 0), 0);
            var visited = new HashSet<(int, int)>();
            while (pq.TryDequeue(out var co, out var prio))
            {
                var (x, y) = co;

                if (visited.Contains(co))
                    continue;
                visited.Add(co);

                cost[co] = prio;

                if (x == cols - 1 && y == rows - 1) // Break at *end* tile
                    break;

                foreach (var (dx, dy) in ddirs)
                {
                    var cc = x + dx;
                    var rr = y + dy;
                    if (!(0 <= cc && cc < cols && 0 <= rr && rr < rows))
                        continue;

                    pq.Enqueue((cc, rr), prio + g[cc, rr]);
                }
            }

            return cost[(cols - 1, rows - 1)];
        }

        int PartTwo(int[,] g)
        {
            var N = g.GetUpperBound(0) + 1;
            var M = g.GetUpperBound(1) + 1;
            var cols = N * 5;
            var rows = M * 5;
            var ddirs = new[] { (0, 1), (0, -1), (1, 0), (-1, 0) };

            var cost = new Dictionary<(int, int), int>();
            var pq = new PriorityQueue<(int x, int y), int>();
            pq.Enqueue((0, 0), 0);
            var visited = new HashSet<(int, int)>();
            var getCost = (int col, int row) =>
            {
                var x = g[col % N, row % M] + (col / N) + (row / M);
                return (x - 1) % 9 + 1;
            };

            while (pq.TryDequeue(out var co, out var prio))
            {
                var (x, y) = co;

                if (visited.Contains(co))
                    continue;
                visited.Add(co);

                cost[co] = prio;

                if (x == cols - 1 && y == rows - 1) // Break at *end* tile
                    break;

                foreach (var (dx, dy) in ddirs)
                {
                    var cc = x + dx;
                    var rr = y + dy;
                    if (!(0 <= cc && cc < cols && 0 <= rr && rr < rows))
                        continue;

                    pq.Enqueue((cc, rr), prio + getCost(cc, rr));
                }
            }

            return cost[(cols - 1, rows - 1)];
        }
    }
}
