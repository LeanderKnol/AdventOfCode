using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Instrumentation;
using AdventOfCode.Instrumentation.DataTypes;
using Dijkstra.NET.Graph.Simple;
using Dijkstra.NET.ShortestPath;

namespace AdventOfCode._2021.Day15
{
    public class Day15
    {
        private readonly string[] _input;
        public Day15()
        {
            //var lines = Helpers.ReadInputArray<string>("2021/Day15/ExampleInput.txt");
            _input = Helpers.ReadInputArray<string>("2021/Day15/Input.txt");
        }

        public void Part1()
        {
            Dictionary<Coordinate2D, int> cave = new();
            for (int y = 0; y < _input.Length; y++)
            {
                for (int x = 0; x < _input[y].Length; x++)
                {
                    cave.Add(new Coordinate2D(x, y), (int)char.GetNumericValue(_input[y][x]));
                }
            }

            var result = GetPath(cave);
            Console.WriteLine(result.Distance);
        }

        public void Part2()
        {
            Dictionary<Coordinate2D, int> cave = new();
            var originalHeight = _input.Length;
            for (int y = 0; y < originalHeight * 5; y++)
            {
                var originalY = y % originalHeight;
                var incrementY = y / originalHeight;
                var originalWidth = _input[originalY].Length;

                for (int x = 0; x < originalWidth * 5; x++)
                {
                    var originalX = x % originalWidth;
                    var incrementX = x / originalWidth;
                    var increment = incrementX + incrementY;

                    var risk = (int)char.GetNumericValue(_input[originalY][originalX]) + increment;
                    if (risk > 9)
                    {
                        risk -= 9;
                    }

                    cave.Add(new Coordinate2D(x, y), risk);
                }
            }
          
            var result = GetPath(cave);
            Console.WriteLine(result.Distance);
        }
       
        ShortestPathResult GetPath(Dictionary<Coordinate2D, int> cave)
        {
            int width = cave.Max(c => c.Key.X)+1;
            int height = cave.Max(c => c.Key.Y)+1;
            var graph = new Graph();

            uint[,] nodes = new uint[width, height];
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    nodes[x, y] = graph.AddNode();
                }
            }

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    if (x < width - 1)
                    {
                        graph.Connect(nodes[x, y], nodes[x + 1, y], cave[(x + 1, y)]);
                    }
                    if (x > 0)
                    {
                        graph.Connect(nodes[x, y], nodes[x - 1, y], cave[(x - 1, y)]);
                    }
                    if (y < height - 1)
                    {
                        graph.Connect(nodes[x, y], nodes[x, y + 1], cave[(x, y + 1)]);
                    }
                    if (y > 0)
                    {
                        graph.Connect(nodes[x, y], nodes[x, y - 1], cave[(x, y - 1)]);
                    }
                }
            }

            return graph.Dijkstra(nodes[0, 0], nodes[width - 1, height - 1]);
        }
    }
}
