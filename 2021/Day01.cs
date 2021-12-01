using AoCRunner.Common;

internal class Day01 : BaseDay
{
    public override int Year => 2021;

    public override int DayNumber => 1;

    public int Part1(int[] input)
    {
        var counter = 0;
        for (int i = 1; i < input.Length; i++)
            if (input[i] > input[i - 1])
                counter = counter + 1;
        return counter;

        //return input.Zip(input.Skip(1)).Aggregate(0, (acc, itm) => itm.Second > itm.First ? acc + 1 : acc);
    }

    public int Part2(int[] input)
    {
        var counter = 0;
        for (int i = 0; i < input.Length - 1; i++)
        {
            if (i + 3 > input.Length - 1)
                break;

            var slice1 = new Span<int>(input, i, 3);
            var slice2 = new Span<int>(input, i + 1, 3);
            var sum1 = 0;
            var sum2 = 0;
            for (int y = 0; y < slice1.Length; y++)
            {
                sum1 = sum1 + slice1[y];
                sum2 = sum2 + slice2[y];
            }

            counter = sum1 < sum2 ? counter + 1 : counter;
        }
        return counter;

        //var x = input.Select((s, i) => input.Skip(i).Take(3)).Where(w => w.Count() == 3);
        //return x.Zip(x.Skip(1)).Aggregate(0, (acc, itm) => itm.Second.Sum() > itm.First.Sum() ? acc + 1 : acc);
    }

    protected override void Run(byte[] input)
    {
        if (input == null) return;

        PartA = $"{Part1(input.GetLines().Select(int.Parse).ToArray())}";
        PartB = $"{Part2(input.GetLines().Select(int.Parse).ToArray())}";
    }
}