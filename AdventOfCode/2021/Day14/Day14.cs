using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Instrumentation;

namespace AdventOfCode._2021.Day14
{
    public class Day14
    {
        private string _template;
        private Dictionary<string, string> _rules;

        private void Run(int steps)
        {
            SetupData();

            for (int step = 1; step <= steps; step++)
            {
                var newTemplate = string.Empty;
                for (int i = 1; i < _template.Length; i++)
                {
                    var pair = _template.Substring(i - 1, 2);
                    string value = _template[i - 1].ToString();
                    if (_rules.ContainsKey(pair))
                    {
                        value = $"{value}{_rules[pair]}";
                    }

                    newTemplate = $"{newTemplate}{value}";
                }
                _template = $"{newTemplate}{_template[^1]}";
            }

            var counts = new Dictionary<char, int>();
            foreach (var character in _template)
            {
                counts.SafeUpdate(character,1);
            }
            Console.WriteLine(counts.Max(c => c.Value) - counts.Min(c => c.Value));
        }

        private void RunBetter(int steps)
        {
            SetupData();

            var pairCount = _rules.Keys.ToDictionary(k => k, v => (long)0);
            var charCount = new Dictionary<string, long>();

            for (int i = 1; i < _template.Length; i++)
            {
                pairCount[_template.Substring(i - 1, 2)]++;
                charCount.SafeIncrement(_template.Substring(i, 1), 1);
            }

            for (int i = 0; i < steps; i++)
            {
                Dictionary<string, long> newPairCount = new Dictionary<string, long>();
                foreach (var pair in pairCount.Where(c => c.Value != 0))
                {
                    string leftCharacter = pair.Key[0].ToString();
                    string rightCharacter = pair.Key[1].ToString();
                    string newCharacter = _rules[pair.Key];

                    newPairCount.SafeIncrement(leftCharacter + newCharacter, pair.Value);
                    newPairCount.SafeIncrement(newCharacter + rightCharacter, pair.Value);
                    charCount.SafeIncrement(newCharacter, pair.Value);
                }
                pairCount = newPairCount;
            }

            Console.WriteLine(charCount.Values.Max() - charCount.Values.Min());
        }

        private void SetupData()
        {
            _template = string.Empty;
            _rules = new Dictionary<string, string>();

            var input = Helpers.ReadInputArray<string>("2021/Day14/ExampleInput.txt");
            //var input = Helpers.ReadInputArray<string>("2021/Day14/Input.txt");

            foreach (var line in input.Where(l => !string.IsNullOrWhiteSpace(l)))
            {
                if (line.Contains("->"))
                {
                    var split = line.Split("->");
                    _rules.Add(split[0].Trim(), split[1].Trim());
                }
                else if (string.IsNullOrWhiteSpace(_template))
                {
                    _template = line;
                }
            }
        }

        public void Part1()
        {
            //Run(10);
            RunBetter(10);
        }

        public void Part2()
        {
            //Run(40);
            RunBetter(40);


        }
    }
}
