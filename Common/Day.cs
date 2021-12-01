using System.Diagnostics;

namespace AoCRunner.Common
{
    internal abstract class Day
    {
        public abstract int Year { get; }
        public abstract int DayNumber { get; }
        protected abstract void ExecuteDay(byte[] input);
         
        protected string PartA { get; set; } = default!;
        protected string PartB { get; set; } = default!;

        protected void Dump(char part, object output) =>
            _output.Add($"Year {Year}, Day {DayNumber}, Part {part}: {output}");

        private readonly List<string> _output = new List<string>();

        public int TotalMicroseconds { get; protected set; }
        public double TotalMilliseconds { get; protected set; }

        public async Task Execute()
        {
            if (Year == 0)
                return;

            var inputFile = $"{Year}\\day{DayNumber:00}.input.txt";
            if (!Directory.Exists(Year.ToString()))
                Directory.CreateDirectory(Year.ToString());
            if (!File.Exists(inputFile))
            {
                var response = await Program.HttpClient.GetAsync($"{Year}/day/{DayNumber}/input");
                response.EnsureSuccessStatusCode();
                File.WriteAllText(inputFile, await response.Content.ReadAsStringAsync());
            }

            var input = File.ReadAllBytes(inputFile);

            ExecuteDay(null!);

            var sw = new Stopwatch();
            sw.Start();
            ExecuteDay(input);
            sw.Stop();

            if (!_output.Any())
            {
                Dump('A', PartA);
                Dump('B', PartB);
            }

            if (Year != 0)
            {
                Console.WriteLine(string.Join(Environment.NewLine, _output));
                Console.WriteLine();
            }

            if (TotalMicroseconds == 0)
                TotalMicroseconds = (int)
                    (sw.Elapsed.TotalMilliseconds * 1000);
            if (TotalMilliseconds == 0)
                TotalMilliseconds = sw.Elapsed.TotalMilliseconds;
        }
    }
}
