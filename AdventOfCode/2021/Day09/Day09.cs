using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Instrumentation;

namespace AdventOfCode._2021.Day09
{
    public class Day09
    {
        private readonly int[][] _map;
        private readonly List<(int x, int y)> _offsets = new() { (0, 1), (1, 0), (0, -1), (-1, 0) };
        public Day09()
        {
            var input = Helpers.ReadInputArray<string>("2021/Day09/Input.txt");
            //var input = Helpers.ReadInputArray<string>("2021/Day09/ExampleInput.txt");

            _map = new int[input.Length][];
            for (int i = 0; i < input.Length; i++)
            {
                _map[i] = input[i].Select(c => int.Parse(c.ToString())).ToArray();
            }

        }

        public void Part1()
        {
            var count = 0;
            for (int i = 0; i < _map.Length; i++)
            {
                for (int j = 0; j < _map[i].Length; j++)
                {
                    var current = _map[i][j];
                    if (_offsets.Count(o => OutOfBounds(i, o, j) || _map[i + o.x][j + o.y] > current) == 4)
                    {
                        count += (current + 1);
                    }

                }
            }

            Console.WriteLine(count);
        }

        private bool OutOfBounds(int x, (int x, int y) o, int y)
        {
            return x + o.x < 0 || y + o.y < 0 || x + o.x >= _map.Length || y + o.y >= _map[x].Length;
        }


        public void Part2()
        {
            var basins = new Dictionary<(int x, int y), int>();

            for (int i = 0; i < _map.Length; i++)
            {
                for (int j = 0; j < _map[i].Length; j++)
                {
                    var currentX = i;
                    var currentY = j;

                    var current = _map[currentX][currentY];
                    if (current == 9) continue;

                    var lowestNeighbor = GetLowestNeighbor(i, j);

                    while (_map[lowestNeighbor.x][lowestNeighbor.y] < current)
                    {
                        currentX = lowestNeighbor.x;
                        currentY = lowestNeighbor.y;
                        current = _map[currentX][currentY];
                        lowestNeighbor = GetLowestNeighbor(currentX, currentY);
                    }

                    if (!basins.ContainsKey((currentX, currentY)))
                    {
                        basins.Add((currentX, currentY), 0);
                    }
                    basins[(currentX, currentY)]++;
                }
            }

            var top = basins.OrderByDescending(b => b.Value).Take(3).Select(b => b.Value);
            Console.WriteLine(top.Aggregate<int, long>(1, (current, t) => current * t));
        }

        private (int x, int y) GetLowestNeighbor(int x, int y)
        {
            var minimum = _offsets.Where(o => !OutOfBounds(x, o, y))
                .Select(o => (x: o.x, y: o.y, val: _map[x + o.x][y + o.y]))
                .OrderBy(o => o.val).First();

            return (x + minimum.x, y + minimum.y);
        }
    }
}
