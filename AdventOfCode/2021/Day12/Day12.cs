using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Instrumentation;

namespace AdventOfCode._2021.Day12
{
    public class Day12
    {
        public Day12()
        {

        }

        private long Run(bool part1)
        {
            //var input = Helpers.ReadInputArray<string>("2021/Day12/ExampleInput.txt");
            var input = Helpers.ReadInputArray<string>("2021/Day12/Input.txt");
            var possiblePaths = new List<List<string>>();

            Dictionary<string, List<string>> optionMapping = new();

            foreach (var line in input)
            {
                var route = line.Split("-");
                if (!optionMapping.ContainsKey(route[0]))
                {
                    optionMapping[route[0]] = new();
                }

                if (!optionMapping.ContainsKey(route[1]))
                {
                    optionMapping[route[1]] = new();
                }

                if (route[1] != "start" && route[0] != "end")
                {
                    optionMapping[route[0]].Add(route[1]);
                }
                if (route[0] != "start" && route[1] != "end")
                {
                    optionMapping[route[1]].Add(route[0]);
                }
            }

            var routes = new Queue<Route>();
            routes.Enqueue(new Route { CaveName = "start" });

            while (routes.Any())
            {
                var route = routes.Dequeue();
                route.Path.Add(route.CaveName);

                if (route.CaveName == "end")
                {
                    possiblePaths.Add((route.Path));
                    continue;
                }

                foreach (var option in optionMapping[route.CaveName])
                {
                    var isSmall = option[0] > 96 && option != "end";
                    var visitTwice = route.VisitedSmallCave;
                    if (isSmall && route.Path.Contains(option))
                    {
                        if (part1 || visitTwice)
                        {
                            continue;
                        }

                        visitTwice = true;

                    }

                    var r = new List<string>();
                    r.AddRange(route.Path);
                    routes.Enqueue(new Route { CaveName = option, Path = r, VisitedSmallCave = visitTwice });
                }
            }

            return possiblePaths.Count;
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
