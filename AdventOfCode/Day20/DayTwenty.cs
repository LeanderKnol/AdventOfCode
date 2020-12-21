using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode.Day20
{
    public static class DayTwenty
    {
        public static string ExecuteFirst()
        {

            var input = GetInputArray();
            var pairs = GetPairs(input);
            var corners = GetCorners(pairs);

            var total = 1L;
            foreach (var n in corners)
            {
                total *= n;
            }

            return total.ToString();
        }

        private static IEnumerable<int> GetCorners(Dictionary<string, List<Tile>> pairs)
        {
            var corners = pairs
                .Where(p => p.Value.Count == 1)
                .Select(p => p.Value.First().Number)
                .GroupBy(n => n)
                .Where(n => n.Count() >= 4)
                .SelectMany(g => g)
                .Distinct();
            return corners;
        }

        private static Dictionary<string, List<Tile>> GetPairs(List<Tile> input)
        {
            var pairs = new Dictionary<string, List<Tile>>();
            foreach (var tile in input)
            {
                foreach (var side in tile.GetSides())
                {
                    if (!pairs.ContainsKey(side))
                    {
                        pairs[side] = new List<Tile>();
                    }

                    pairs[side].Add(tile);
                }
            }

            return pairs;
        }


        public static string ExecuteSecond()
        {
            var input = GetInputArray();
            var pairs = GetPairs(input);
            var corners = GetCorners(pairs);


            var sideSize = (int)Math.Sqrt(input.Count);
            var image = new Tile[sideSize][];

            for (var x = 0; x < sideSize; x++)
            {
                image[x] = new Tile[sideSize];
                for (var y = 0; y < sideSize; y++)
                {
                    if (x == 0 && y == 0)
                    {
                        image[x][y] = input.Single(t => t.Number == corners.First());
                        break;
                    }

                    var matchinSides = DetermineMatchingSides(x, y, sideSize);
                }
            }

            return string.Empty;
        }

        private static int DetermineMatchingSides(int x, int y, int sideSize)
        {
            var machingSides = 8;
            if (x == 0 || y == 0)
            {
                machingSides -= 2;
            }

            if (x == (sideSize - 1) || y == (sideSize - 1))
            {
                machingSides -= 2;
                if (x == (sideSize - 1) && y == (sideSize - 1))
                {
                    machingSides -= 2;
                }
            }

            return machingSides;
        }


        private static List<Tile> GetInputArray()
        {
            FileStream fileStream = new FileStream("Day20/input.txt", FileMode.Open);
            using StreamReader reader = new StreamReader(fileStream);

            var result = new List<Tile>();

            string input;

            var currentTile = new Tile();
            while ((input = reader.ReadLine()) != null)
            {
                if (input.Contains("Tile"))
                {
                    currentTile.Number = int.Parse(input.Replace("Tile", string.Empty).Replace(":", string.Empty));

                }
                else if (string.IsNullOrWhiteSpace(input))
                {
                    result.Add(currentTile);
                    currentTile = new Tile();
                }
                else
                {
                    currentTile.Image.Add(input);
                }
            }
            result.Add(currentTile);
            return result;
        }


    }
}
