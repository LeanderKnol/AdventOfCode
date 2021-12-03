using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode._2020.Day24
{
    public static class DayTwentyFour
    {
        private static readonly Dictionary<string, (double X, double Y)> Directions = new Dictionary<string, (double, double)>
        {
            {"e", (1,0) },
            {"w", (-1,0) },
            { "se", (0.5, -1) },
            { "sw", (-0.5, -1) },
            { "nw", (-0.5, 1) },
            { "ne", (0.5, 1) },
        };

        public static string ExecuteFirst()
        {
            var lines = GetInputArray();
            var endCoordinates = GetEndCoordinates(lines);
            var group = endCoordinates.GroupBy(ec => ec).ToDictionary(x => x.Key, x => x.Count() % 2 == 0 ? 'W' : 'B');
            return group.Count(g => g.Value == 'B').ToString();
        }

        private static List<(double X, double Y)> GetEndCoordinates(List<string> lines)
        {
            var endCoordinates = new List<(double X, double Y)>();
            foreach (var line in lines)
            {
                var lineSteps = new List<string>();
                var searchString = string.Empty;

                foreach (var character in line)
                {
                    searchString += character;
                    if (Directions.ContainsKey(searchString))
                    {
                        lineSteps.Add(searchString);
                        searchString = string.Empty;
                    }
                }

                var location = (X: 0.0, Y: 0.0);
                lineSteps.Select(x => Directions[x]).ToList().ForEach(x => location = (location.X + x.X, location.Y + x.Y));
                endCoordinates.Add(location);
            }

            return endCoordinates;
        }


        public static string ExecuteSecond()
        {
            var lines = GetInputArray();
            var endCoordinates = GetEndCoordinates(lines);
            var situation = endCoordinates.GroupBy(ec => ec).ToDictionary(x => x.Key, x => x.Count() % 2 == 0 ? 'W' : 'B');
            var neighbourCoordinates = Directions.Values.Select(x => x).ToList();


            for (int i = 0; i < 100; i++)
            {
                var newSituation = new Dictionary<(double X, double Y), char>(situation);
                var keys = newSituation.Keys.ToHashSet();

                foreach (var tile in situation.Keys)
                {
                    var neighbours = neighbourCoordinates.Select(x => (x.X + tile.X, x.Y + tile.Y)).ToList();
                    neighbours.ForEach(tile =>
                    {
                        newSituation[tile] = UpdateTile(tile, keys, situation);
                    });

                    newSituation[tile] = UpdateTile(tile, keys, situation);
                }

                situation = new Dictionary<(double X, double Y), char>(newSituation);
            }
            return situation.Count(g => g.Value == 'B').ToString();
        }


        private static char UpdateTile((double X, double Y) tile, HashSet<(double X, double Y)> keys, Dictionary<(double X, double Y), char> situation)
        {
            var exists = keys.Contains(tile);
            var inactiveNeighbours = Directions.Values.Select(x => (x.X + tile.X, x.Y + tile.Y)).Count(item => keys.Contains(item) && situation[item] == 'B');
            var isInactive = exists && situation[tile] == 'B';

            if (isInactive && (inactiveNeighbours == 0 || inactiveNeighbours > 2))
            {
                return 'W';
            }
            else if (!isInactive && inactiveNeighbours == 2)
            {
                return 'B';
            }

            return !exists ? 'W' : situation[tile];
        }

        private static List<string> GetInputArray()
        {
            return File.ReadAllLines("Day24/input.txt").ToList();
        }


    }
}
