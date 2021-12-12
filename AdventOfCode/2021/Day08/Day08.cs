using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Instrumentation;

namespace AdventOfCode._2021.Day08
{
    public class Day08
    {
        private readonly List<Entry> _entries = new();

        public Day08()
        {
            var input = Helpers.ReadInputArray<string>("2021/Day08/Input.txt");
            //var input = Helpers.ReadInputArray<string>("2021/Day08/ExampleInput.txt");

            foreach (var entry in input)
            {
                var split = entry.Split("|");
                var signals = split[0].Trim().Split(" ").ToDictionary<string, string, int?>(s => string.Concat(s.OrderBy(c => c)), s => null);
                var output = split[1].Trim().Split(" ").Select(s => string.Concat(s.OrderBy(c => c))).ToList();
                _entries.Add(new Entry { Signals = signals, Output = output });
            }

        }

        public void Part1()
        {
            var count = 0;
            foreach (var entry in _entries)
            {
                count += entry.Output.Count(o => o.Length is 2 or 3 or 4 or 7);
            }

            Console.WriteLine(count);
        }


        public void Part2()
        {
            long total = 0;

            foreach (var entry in _entries)
            {
                entry.Signals[entry.Signals.First(p => p.Key.Length == 2).Key] = 1;
                entry.Signals[entry.Signals.First(p => p.Key.Length == 4).Key] = 4;
                entry.Signals[entry.Signals.First(p => p.Key.Length == 3).Key] = 7;
                entry.Signals[entry.Signals.First(p => p.Key.Length == 7).Key] = 8;

                CalculateNineZeroSix(entry);
                CalculateTwoThreeFive(entry);

                var result = string.Empty;
                foreach (var output in entry.Output)
                {
                    result += entry.Signals[output].Value;
                }

                total += int.Parse(result);
            }

            Console.WriteLine(total);
        }

        private void CalculateNineZeroSix(Entry entry)
        {
            //Get entries with 6 elements. Only one of those contains the signals of the 4, that is number 9.
            var zeroSixNine = entry.Signals.Where(p => p.Key.Length == 6);
            var four = entry.Signals.Single(s => s.Value == 4);

            foreach (var signal in zeroSixNine)
            {
                if (four.Key.All(signalPart => signal.Key.Contains(signalPart)))
                {
                    entry.Signals[signal.Key] = 9;
                    zeroSixNine = zeroSixNine.Where(zsn => zsn.Key != signal.Key);
                    break;
                }
            }

            //When the 9 is found only one of the remaining contains the signals of 1, that is number 0;
            var one = entry.Signals.Single(s => s.Value == 1);
            foreach (var signal in zeroSixNine)
            {
                if (one.Key.All(signalPart => signal.Key.Contains(signalPart)))
                {
                    entry.Signals[signal.Key] = 0;
                    zeroSixNine = zeroSixNine.Where(zsn => zsn.Key != signal.Key);
                    break;
                }
            }

            //The one remaining is 6
            entry.Signals[zeroSixNine.Single().Key] = 6;
        }

        private void CalculateTwoThreeFive(Entry entry)
        {
            //Get entries with 6 elements. Only one of those contains the signals of the 1, that is number 3.
            var twoThreeFive = entry.Signals.Where(p => p.Key.Length == 5);
            var one = entry.Signals.Single(s => s.Value == 1);

            foreach (var signal in twoThreeFive)
            {
                if (one.Key.All(signalPart => signal.Key.Contains(signalPart)))
                {
                    entry.Signals[signal.Key] = 3;
                    twoThreeFive = twoThreeFive.Where(zsn => zsn.Key != signal.Key);
                    break;
                }
            }

            //The number 5 only differs one element from the 6, the number two differs two. 
            var six = entry.Signals.Single(s => s.Value == 6);
            foreach (var signal in twoThreeFive)
            {
                var noMath = six.Key.Count(signalPart => !signal.Key.Contains(signalPart));
                if (noMath == 1)
                {
                    entry.Signals[signal.Key] = 5;
                }
                if (noMath == 2)
                {
                    entry.Signals[signal.Key] = 2;

                }
            }

        }
    }
}
