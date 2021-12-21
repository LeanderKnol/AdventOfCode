using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Instrumentation;
using AdventOfCode.Instrumentation.DataTypes;

namespace AdventOfCode._2021.Day11
{
    public class Day11
    {
        public Day11()
        {
        }

        private long Run(bool part1)
        {
            long rounds = 0;
            Dictionary<Coordinate2D, int> octopuses = new();

            var input = Helpers.ReadInputArray<string>("2021/Day11b/Input.txt");
            //var input = Helpers.ReadInputArray<string>("2021/Day11b/ExampleInput.txt");

            for (var x = 0; x < input.Length; x++)
            {
                var row = input[x].Select(c => int.Parse(c.ToString())).ToArray();
                for (int y = 0; y < row.Length; y++)
                {
                    octopuses.Add((x, y), row[y]);
                }
            }

            long totalFlashes = 0;
            while (true)
            {
                foreach (var n in octopuses.Keys.ToList())
                {
                    octopuses[n]++;
                };

                var flashedOctopuses = octopuses.ToDictionary(x => x.Key, y => false);
                while (octopuses.Any(x => x.Value > 9 && !flashedOctopuses[x.Key]))
                {
                    var flashers = octopuses.Where(x => x.Value > 9 && !flashedOctopuses[x.Key]).Select(x => x.Key).ToArray();
                    foreach (var f in flashers)
                    {
                        totalFlashes++;
                        flashedOctopuses[f] = true;

                        foreach (var n in f.Neighbors(true))
                        {
                            if (octopuses.ContainsKey(n))
                            {
                                octopuses[n]++;
                            };
                        }
                    }
                }

                foreach (var octopus in flashedOctopuses.Where(x => x.Value == true))
                {
                    octopuses[octopus.Key] = 0;
                }

                rounds++;
                if (part1 && rounds == 100)
                {
                    return totalFlashes;
                };
                if (flashedOctopuses.Count(x => x.Value) == octopuses.Count)
                {
                    break;
                };
            }

            return rounds;
        }

        public void Part1()
        {
            Console.WriteLine(Run(true));
        }

        public void Part2()
        {
            Console.WriteLine(Run(false));

        }
    }
}
