﻿using System;
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
            var edges = pairs.Where(p => p.Value.Count == 1).Select(p => p.Key).ToList();


            var sideSize = (int)Math.Sqrt(input.Count);
            var image = new Tile[sideSize][];

            //Assembling image
            for (var y = 0; y < sideSize; y++)
            {
                image[y] = new Tile[sideSize];
                for (var x = 0; x < sideSize; x++)
                {
                    if (y == 0 && x == 0)
                    {
                        var firstCorner = input.Single(i => i.Number == corners.First());
                        CheckCorners();

                        image[y][x] = firstCorner;

                        void CheckCorners()
                        {
                            for (int i = 0; i < 4; i++)
                            {
                                if (edges.Contains(firstCorner.Top) && edges.Contains(firstCorner.Left))
                                {
                                    break;
                                }

                                firstCorner.Rotate();
                            }
                        }
                    }
                    else
                    {
                        if (x == 0)
                        {
                            var topTile = image[y - 1][x];
                            var neighbour = pairs[topTile.Bottom].Single(t => t.Number != topTile.Number);
                            if (FixOrientation(neighbour, topTile.Bottom, null))
                            {
                                image[y][x] = neighbour;
                            }
                        }
                        else
                        {
                            var leftTile = image[y][x - 1];
                            var neighbour = pairs[leftTile.Right].Single(t => t.Number != leftTile.Number);
                            if (FixOrientation(neighbour, null, leftTile.Right))
                            {
                                image[y][x] = neighbour;
                            }
                        }

                    }

                }
            }

            //String edges
            var strippedImage = new List<string>();
            foreach (var row in image)
            {
                var stringedFragmentRow = new string[8];
                foreach (var tile in row)
                {
                    for (int i = 1; i < 9; i++)
                    {
                        stringedFragmentRow[i - 1] += tile.Image[i][1..9];
                    }


                }
                strippedImage.AddRange(stringedFragmentRow);
            }

            return string.Empty;
        }

        private static bool FixOrientation(Tile tile, string top, string left)
        {
            var orientationCorrect = false;
            CheckEdges();

            if (!orientationCorrect)
            {
                tile.FlipHorizontal();
                CheckEdges();
            }
            if (!orientationCorrect)
            {
                tile.FlipVertical();
                CheckEdges();
            }

            return orientationCorrect;

            void CheckEdges()
            {
                for (int i = 0; i < 4; i++)
                {
                    if ((top == null || top == tile.Top) && (left == null || left == tile.Left))
                    {
                        orientationCorrect = true;
                        break;
                    }

                    tile.Rotate();
                }
            }
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
