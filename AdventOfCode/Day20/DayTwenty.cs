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
            var strippedImage = new Tile
            {
                TileSize = 96
            };
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
                strippedImage.Image.AddRange(stringedFragmentRow);
            }

            var monster = new[]{
                "                  # ",
                "#    ##    ##    ###",
                " #  #  #  #  #  #   "
            };

            var totalMonsters = 0;
            CheckRotating();

            if (totalMonsters == 0)
            {
                strippedImage.FlipHorizontal();
                CheckRotating();
            }

            if (totalMonsters == 0)
            {
                strippedImage.FlipVertical();
                CheckRotating();
            }

            void CheckRotating()
            {
                for (int i = 0; i < 4; i++)
                {
                    totalMonsters = SearchMonster(strippedImage.Image, monster);
                    if (totalMonsters > 0)
                    {
                        break;
                    }

                    strippedImage.Rotate();
                }
            }

            if (totalMonsters > 0)
            {
                var totalHashes = strippedImage.Image.Sum(row => row.Count(ch => ch == '#'));
                var monsterHashes = string.Join("\n", monster).Count(ch => ch == '#');
                return (totalHashes - totalMonsters * monsterHashes).ToString();
            }
            return string.Empty;
        }

        private static int SearchMonster(List<string> strippedImage, string[] monster)
        {
            var totalMonsters = 0;

            for (var y = 0; y < strippedImage.Count - monster.Length; y++)
            {
                for (var x = 0; x < strippedImage.First().Length - monster[0].Length; x++)
                {
                    if (MatchMonster())
                    {
                        totalMonsters++;
                    }

                    bool MatchMonster()
                    {
                        for (var mY = 0; mY < monster.Length; mY++)
                            for (var mX = 0; mX < monster[0].Length; mX++)
                            {
                                if (monster[mY][mX] == '#' && strippedImage[y + mY][x + mX] != '#')
                                {
                                    return false;
                                }
                            }

                        return true;
                    }
                }
            }

            return totalMonsters;
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
