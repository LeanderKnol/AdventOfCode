using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Instrumentation;

namespace AdventOfCode._2021.Day10

{
    public class Day10
    {
        private readonly Dictionary<char, char> _pairs = new() { { '(', ')' }, { '[', ']' }, { '{', '}' }, { '<', '>' } };
        private readonly string[] _input;
        private readonly List<string> _validInput = new List<string>();

        public Day10()
        {
            //_input = Helpers.ReadInputArray<string>("2021/Day10/ExampleInput.txt");
            _input = Helpers.ReadInputArray<string>("2021/Day10/Input.txt");

        }

        public void Part1()
        {
            var penalties = new Dictionary<char, int> { { ')', 3 }, { ']', 57 }, { '}', 1197 }, { '>', 25137 } };
            long count = 0;

            foreach (var line in _input)
            {
                var invalid = false;
                Stack<char> opened = new Stack<char>();
                foreach (var c in line)
                {
                    if (_pairs.ContainsKey(c))
                    {
                        opened.Push(_pairs[c]);
                    }
                    else if (opened.Pop() != c)
                    {
                        count += penalties[c];
                        invalid = true;
                        break;
                    }
                }
                if (!invalid)
                {
                    _validInput.Add(line);
                }
            }

            Console.WriteLine(count);
        }




        public void Part2()
        {
            var scores = new List<long>();
            var penalties = new Dictionary<char, int> { { ')', 1 }, { ']', 2 }, { '}', 3 }, { '>', 4 } };
            foreach (var line in _validInput)
            {
                Stack<char> opened = new Stack<char>();
                foreach (var c in line)
                {
                    if (_pairs.ContainsKey(c))
                    {
                        opened.Push(_pairs[c]);
                    }
                    else
                    {
                        opened.Pop();
                    }
                }

                long count = 0;
                foreach (var o in opened)
                {
                    count *= 5;
                    count += penalties[o];
                }
                scores.Add(count);
            }

            scores = scores.OrderBy(s => s).ToList();
            var middel = Math.Ceiling((decimal)(scores.Count / 2));
            Console.WriteLine(scores[Decimal.ToInt32(middel)]);

        }

    }
}
