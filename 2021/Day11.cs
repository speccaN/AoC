using AoCRunner.Common;

namespace AoCRunner._2021
{
    internal class Day11 : BaseDay
    {
        public override int Year => 2021;

        protected override void Run(byte[] input)
        {
            if (input == null) return;

            var raw_input = input.GetLines();

            var grid = new int[raw_input.Length, raw_input[0].Length];

            for (int y = 0; y < raw_input.Length; y++)
                for (int x = 0; x < raw_input[y].Length; x++)
                    grid[x, y] = (int)char.GetNumericValue(raw_input[y][x]);


            var ddirs = new[] { -1, 0, 1 };
            var ans = 0;
            var index = 0;
            while (true)
            {
                index++;
                for (int y = 0; y <= grid.GetUpperBound(0); y++)
                    for (int x = 0; x <= grid.GetUpperBound(1); x++)
                        grid[x, y] += 1;

                for (int y = 0; y <= grid.GetUpperBound(0); y++)
                    for (int x = 0; x <= grid.GetUpperBound(1); x++)
                        if (grid[x, y] == 10)
                            flash(x, y);

                var done = true;
                for (int y = 0; y <= grid.GetUpperBound(0); y++)
                    for (int x = 0; x <= grid.GetUpperBound(1); x++)
                        if (grid[x, y] == -1)
                            grid[x, y] = 0;
                        else
                            done = false;

                if (index == 100)
                    PartA = $"{ans}";

                if (done)
                {
                    PartB = $"{index}";
                    break;
                }
            }


            void flash(int x, int y)
            {
                ans += 1;
                grid[x, y] = -1;

                foreach (var dr in ddirs)
                {
                    foreach (var dc in ddirs)
                    {
                        var cc = x + dc;
                        var rr = y + dr;

                        if (0 <= rr && rr <= grid.GetUpperBound(0) && 0 <= cc && cc <= grid.GetUpperBound(1) && grid[cc, rr] != -1)
                        {
                            grid[cc, rr] += 1;
                            if (grid[cc, rr] >= 10)
                                flash(cc, rr);
                        }
                    }
                }
            }
        }
    }
}
