using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using AdventOfCode.Instrumentation;
using AdventOfCode.Instrumentation.DataTypes;

namespace AdventOfCode._2021.Day05
{
    public class Day05
    {
        private readonly List<string> _input;
        private Dictionary<Coordinate2D, int> _map;
        public Day05()
        {
            _input = Helpers.ReadInputArray<string>("2021/Day05/Input.txt").ToList();
            //_input = Helpers.ReadInputArray<string>("2021/Day05/ExampleInput.txt").ToList();

        }

        public void Part1()
        {
            Calculate(true);
        }  

        public void Part2()
        {
            Calculate(false);
        }

        private void Calculate(bool noDiagonal)
        {
            _map = new();
            foreach (var input in _input)
            {
                int[] coords = Regex.Split(input, @"[^\d]+").Select(int.Parse).ToArray();
                (int x1, int y1, int x2, int y2) = (coords[0], coords[1], coords[2], coords[3]);

                int directionX = x1 == x2 ? 0 : x1 > x2 ? -1 : 1;
                int directionY = y1 == y2 ? 0 : y1 > y2 ? -1 : 1;
                if (noDiagonal && directionX != 0 && directionY != 0)
                {
                    continue;;
                }
                (int x, int y) = (x1, y1);
                while (true)
                {
                    if (!_map.ContainsKey((x, y))) _map[(x, y)] = 0;
                    _map[(x, y)]++;
                    if ((x, y) == (x2, y2))
                    {
                        break;
                    }

                    x += directionX;
                    y += directionY;
                }
            }

            Console.WriteLine(_map.Count(m => m.Value > 1));
        }
    }
}
