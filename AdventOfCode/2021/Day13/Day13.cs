using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Instrumentation;

namespace AdventOfCode._2021.Day13
{
    public class Day13
    {
        private List<Coordinate2D> _grid = new();
        private readonly List<(int line, string type)> _folds = new();
        public Day13()
        {
            //var input = Helpers.ReadInputArray<string>("2021/Day13/ExampleInput.txt");
            var input = Helpers.ReadInputArray<string>("2021/Day13/Input.txt");
            foreach (var line in input.Where(l => !string.IsNullOrWhiteSpace(l)))
            {
                if (line.Contains("fold along"))
                {
                    var split = line.Replace("fold along", string.Empty).Trim().Split("=");
                    _folds.Add((split[1].ToInt(), split[0]));
                }
                else
                {
                    var coordinates = line.Split(',');
                    _grid.Add(new Coordinate2D(coordinates[0].ToInt(), coordinates[1].ToInt()));
                }
            }

        }

        private void Run(bool part1)
        {
            var folds = part1 ? _folds.Take(1) : _folds;
            foreach (var fold in folds)
            {
                var newGrid = new List<Coordinate2D>();

                foreach (var coordinate in _grid)
                {
                    var x = coordinate.x;
                    var y = coordinate.y;
                    if (fold.type == "x")
                    {
                        if (coordinate.x >= fold.line)
                        {
                            x = (fold.line - (coordinate.x - fold.line));
                        }
                    }
                    else
                    {
                        if (coordinate.y >= fold.line)
                        {
                            y = (fold.line - (coordinate.y - fold.line));
                        }
                    }

                    var newCoordinate = new Coordinate2D(x, y);
                    if (!newGrid.Contains(newCoordinate))
                    {
                        newGrid.Add(newCoordinate);
                    }
                }

                _grid = newGrid;
            }
        }

        public void Part1()
        {
            Run(true);
            Console.WriteLine(_grid.Count);
        }

        public void Part2()
        {
            Run(false);

            var maxX = _grid.Max(c => c.x);
            var maxY = _grid.Max(c => c.y);

            for (int i = 0; i <= maxY; i++)
            {
                for (int j = 0; j <= maxX; j++)
                {
                    if (_grid.Contains(new Coordinate2D(j, i)))
                    {
                        Console.Write("#");
                    }
                    else
                    {
                        Console.Write(" ");
                    }
                }
                Console.WriteLine();
            }

            Console.WriteLine();

        }
    }
}
