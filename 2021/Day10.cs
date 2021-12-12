using AoCRunner.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoCRunner._2021
{
    internal class Day10 : BaseDay
    {
        public override int Year => 2021;

        protected override void Run(byte[] input)
        {
            if (input == null) return;

            var raw_data = input.GetLines();
            var closing = new Dictionary<char, char>
            {
                {'[', ']'},
                {'(', ')'},
                {'{', '}' },
                {'<', '>' }
            };

            var cl = new HashSet<char>
            {
                {']'},
                {')'},
                {'}'},
                {'>'}
            };
            var op = new HashSet<char>
            {
                {'(' },
                {'[' },
                {'{' },
                {'<' }
            };
            var score = new Dictionary<char, int>
            {
                {')', 3},
                {']', 57},
                {'}', 1197},
                {'>', 25137}
            };

            var invalid = new List<string>();
            var incomplete = new List<string>();
            var ans = 0;
            foreach (var line in raw_data)
            {
                var stack = new Stack<char>();
                for (int i = 0; i <= line.Length; i++)
                {
                    if (i == line.Length && stack.Any())
                    {
                        incomplete.Add(new string(stack.ToArray()));
                        break;
                    }

                    if (op.Contains(line[i]))
                    {
                        stack.Push(line[i]);
                        continue;
                    }
                    if(cl.Contains(line[i]))
                    {
                        if (closing[stack.Pop()] == line[i])
                            continue;
                        else
                        {
                            invalid.Add(line);
                            ans+=score[line[i]];
                            break;
                        }
                    }
                }
            }

            PartA = $"{ans}";

            score = new Dictionary<char, int>
            {
                {')', 1},
                {']', 2},
                {'}', 3},
                {'>', 4}
            };
            var total_score = new List<long>(incomplete.Count);
            foreach (var line in incomplete)
            {
                var line_score = 0L;
                var stack = new Stack<char>(line.Reverse());

                while (stack.Count > 0)
                {
                    line_score = (5 * line_score) +score[closing[stack.Pop()]];
                }
                total_score.Add(line_score);
            }

            var x = total_score.OrderBy(o => o).ToArray()[total_score.Count/2];
            PartB = $"{x}";
        }
    }
}
