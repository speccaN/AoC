using System.Text;

namespace AoCRunner.Common
{
    internal static class Helpers
    {
        public static string GetString(this byte[] input) =>
            Encoding.ASCII.GetString(input);

        private static readonly string[] _splitChars = new[] { "\r\n", "\n", };
        public static string[] GetLines(this byte[] input, StringSplitOptions options = StringSplitOptions.RemoveEmptyEntries) =>
            GetString(input)
                .Split(_splitChars, options);

        public static int[] GetIntArray(this byte[] input) => GetString(input).Split(",").Select(int.Parse).ToArray();
    }
}
