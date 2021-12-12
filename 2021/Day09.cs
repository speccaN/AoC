using AoCRunner.Common;

namespace AoCRunner._2021
{
    internal class Day09 : BaseDay
    {
        public override int Year => 2021;

        protected override void Run(byte[] input)
        {
            if (input == null) return;

            var rawData = input.GetLines();
            var dirs = new[] { (0, 1), (0, -1), (-1, 0), (1, 0) };
            var grid = rawData.Select(line => line.Select(val => (int)char.GetNumericValue(val)).ToArray()).ToArray();
            var rows = grid.Length;
            var cols = grid[0].Length;
            var ans = 0;
            var low = new List<(int, int)>();
            var currentId = 0;
            var ids = new Dictionary<(int, int), int>();

            for (var row = 0; row < rows; row++)
            {
                for (int col = 0; col < cols; col++)
                {
                    var isLow = true;
                    foreach (var (x, y) in dirs)
                    {
                        var rr = row + y;
                        var cc = col + x;

                        // Check bounds
                        if (!(0 <= rr && rr < rows && 0 <= cc && cc < cols))
                            continue;

                        // Check adjacent
                        if (grid[rr][cc] <= grid[row][col])
                        {
                            isLow = false;
                            break;
                        }
                    }

                    if (isLow)
                        ans += grid[row][col] + 1;
                }
            }
            PartA = $"{ans}";

            // PART 2
            for (var row = 0; row < rows; row++)
            {
                for (int col = 0; col < cols; col++)
                {
                    var is_low = true;
                    foreach (var (x, y) in dirs)
                    {
                        var rr = row + y;
                        var cc = col + x;

                        // Check bounds
                        if (!(0 <= rr && rr < rows && 0 <= cc && cc < cols))
                            continue;

                        // Check adjacent
                        if (grid[rr][cc] <= grid[row][col])
                        {
                            is_low = false;
                            break;
                        }
                    }

                    if (is_low)
                        low.Add((row, col));
                }
            }

            foreach (var (row, col) in low)
            {
                var stack = new Stack<(int, int)>();
                var visited = new List<(int, int)>();
                stack.Push((row, col));

                while (stack.Count > 0)
                {
                    var row_col = stack.Pop();

                    if (visited.Contains(row_col))
                        continue;
                    visited.Add(row_col);

                    ids[row_col] = currentId;

                    foreach (var (x, y) in dirs)
                    {
                        var (rr, cc) = row_col;
                        rr += y;
                        cc += x;

                        // Check bounds
                        if (!(0 <= rr && rr < rows && 0 <= cc && cc < cols))
                            continue;
                        if (grid[rr][cc] == 9)
                            continue;

                        // Check adjacent
                        if (grid[rr][cc] > grid[row_col.Item1][row_col.Item2])
                        {
                            stack.Push((rr, cc));
                        }
                    }
                }
                currentId += 1;
            }

            ans = ids.GroupBy(g => g.Value)
                .OrderByDescending(o => o.Count())
                .Take(3)
                .Select(s => s.Count())
                .Aggregate((a, b) => a * b);
            PartB = $"{ans}";
        }
    }
}
