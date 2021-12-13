using AoCRunner.Common;
using Microsoft.Extensions.Configuration;
using System.Net;
using System.Reflection;

public static class Program
{
    public static HttpClient HttpClient { get; private set; } = null!;

    public static async Task Main(string[] args)
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddCommandLine(args)
            .AddJsonFile("appsettings.json", true, true);

        var config = builder.Build();
        var sessionId = config["SessionId"];

        if (string.IsNullOrEmpty(sessionId))
            throw new ArgumentNullException(nameof(sessionId), "Provide an AoC session id in the appsettings file");

        var baseAddress = new Uri("https://adventofcode.com");
        var cookies = new CookieContainer();
        cookies.Add(baseAddress, new Cookie("session", sessionId));

        HttpClient = new HttpClient(
            new HttpClientHandler
            {
                CookieContainer = cookies,
                AutomaticDecompression = DecompressionMethods.All
            })
        {
            BaseAddress = baseAddress,
        };

        var days = GetDays();
        if (config["y"] != null)
        {
            var year = int.Parse(config["y"]);
            days = days.Where(w => w.Year == year);
        }

        if (config["d"] != null)
        {
            var day = int.Parse(config["d"]);
            days = days.Where(w => w.DayNumber == day);
        }

        foreach (var day in days)
            await day.Execute();

        Console.WriteLine();

        const int YearAlignment = -9;
        const int DayAlignment = 2;
        const int TimeAlignment = 14;
        foreach (var g in days.GroupBy(grp => grp.Year))
        {
            Console.WriteLine(
                string.Join(Environment.NewLine,
                g.Select(s => $"Year {s.Year, YearAlignment} Day {s.DayNumber,DayAlignment} {s.TotalMicroseconds,TimeAlignment:n0} μs")));

            Console.WriteLine(new string(Enumerable.Repeat('-', 40).ToArray()));
            Console.WriteLine(
                $"Year {g.Key, YearAlignment} Total: {g.Sum(s => s.TotalMicroseconds),TimeAlignment:N0} μs");
        }
    }

    private static IEnumerable<BaseDay> GetDays() =>
#pragma warning disable CS8619 // Nullability of reference types in value doesn't match target type.
        Assembly.GetExecutingAssembly().GetTypes()
        .Where(w => w.BaseType == typeof(BaseDay))
        .Select(s => Activator.CreateInstance(s) as BaseDay)
        .OrderBy(o => o!.Year)
        .ThenBy(o => o!.DayNumber)
        .ToArray();
#pragma warning restore CS8619 // Nullability of reference types in value doesn't match target type.
}