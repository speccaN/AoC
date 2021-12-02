using AoCRunner.Common;

namespace AoCRunner._2021
{
    internal class Day02 : BaseDay
    {
        public override int Year => 2021;

        public override int DayNumber => 2;

        int PartOne((string dir, int dist)[] input)
        {
            var pos = (x: 0, y: 0);

            for (int x = 0; x < input.Length; x++)
            {
                switch (input[x].dir)
                {
                    case "down":
                        pos.y = pos.y + input[x].dist;
                        break;
                    case "up":
                        pos.y = pos.y - input[x].dist;
                        break;
                    case "forward":
                        pos.x = pos.x + input[x].dist;
                        break;
                    default:
                        break;
                }
            }
            return pos.y * pos.x;
        }

        private int PartTwo((string dir, int dist)[] input)
        {
            var pos = (x: 0, y: 0);
            var aim = 0;

            for (int x = 0; x < input.Length; x++)
            {
                switch (input[x].dir)
                {
                    case "down":
                        aim = aim + input[x].dist;
                        break;
                    case "up":
                        aim = aim - input[x].dist;
                        break;
                    case "forward":
                        pos.x = pos.x + input[x].dist;
                        pos.y = pos.y + (aim * input[x].dist);
                        break;
                    default:
                        break;
                }
            }

            return pos.y * pos.x;
        }

        protected override void Run(byte[] input)
        {
            if (input == null) return;

            var parsedInput = input.GetLines()
                .Select<string, (string dir, int dist)>(s =>
                {
                    var y = s.Split(" ");
                    return new(y[0], int.Parse(y[1]));
                }).ToArray();

            PartA = $"{PartOne(parsedInput)}";
            PartB = $"{PartTwo(parsedInput)}";
        }
    }
}
