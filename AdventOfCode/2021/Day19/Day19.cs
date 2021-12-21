using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Instrumentation;
using AdventOfCode.Instrumentation.DataTypes;

namespace AdventOfCode._2021.Day19
{
    public class Day19
    {
        private readonly List<List<Coordinate3D>> _scans;
        private readonly List<List<Coordinate3D>> _scanners;
        private readonly List<Coordinate3D> _axes = new List<Coordinate3D> { new(0, 1, 0), new(0, -1, 0), new(1, 0, 0), new(-1, 0, 0), new(0, 0, 1), new(0, 0, -1) };

        public Day19()
        {
            var input =
                //Helpers.ReadInputArray<string>("2021/Day19/ExampleInput.txt")
                Helpers.ReadInputArray<string>("2021/Day19/Input.txt")
                    .Aggregate(new List<List<Coordinate3D>>(), (list, line) =>
                    {
                        if (string.IsNullOrWhiteSpace(line))
                        {
                            return list;
                        }

                        if (line.StartsWith("---"))
                        {
                            list.Add(new List<Coordinate3D>());
                            return list;
                        }

                        var parts = line.Split(new[] { ',' });
                        list[^1].Add((parts[0].ToInt(), parts[1].ToInt(), parts[2].ToInt()));
                        return list;
                    });


            var scans = input;
            var scanners = input.Select(_ => new List<Coordinate3D> { (0, 0, 0) }).ToList();

            while (scans.Count > 1)
            {
                (scans, scanners) = Calculate(scans, scanners);
            }

            _scans = scans;
            _scanners = scanners;
        }

        public void Part1()
        {
            Console.WriteLine(_scans[0].ToList().Count);
        }

        public void Part2()
        {

            var scannerList = _scanners[0];
            var farthest = Enumerable.Range(0, scannerList.Count - 1)
                    .SelectMany(i => Enumerable.Range(i + 1, scannerList.Count - i - 1).Select(j => (i, j)))
                    .Max(pair => scannerList[pair.i].ManhattanDistance(scannerList[pair.j]));

            Console.WriteLine(farthest);
        }

        private (List<List<Coordinate3D>> scans, List<List<Coordinate3D>> scanners) Calculate(List<List<Coordinate3D>> scans, List<List<Coordinate3D>> scanners)
        {
            var toRemove = new List<int>();
            for (int i = 0; i < scans.Count - 1; i++)
            {
                for (int j = i + 1; j < scans.Count; j++)
                {
                    if (toRemove.Contains(j))
                    {
                        continue;
                    }

                    var alignment = Align(scans[i], scans[j]);
                    if (alignment != null)
                    {
                        foreach (var s in scanners[j])
                        {
                            var scanner = alignment.Value.translation + Transform(s, alignment.Value.up, alignment.Value.rotation);
                            scanners[i].Add(scanner);
                        }

                        scans[i] = scans[i].Union(alignment.Value.alignedBeacons).ToList();
                        toRemove.Add(j);
                    }
                }
            }
            return (scans.Where((el, i) => !toRemove.Contains(i)).ToList(), scanners.Where((el, i) => !toRemove.Contains(i)).ToList());
        }

        private (List<Coordinate3D> alignedBeacons, Coordinate3D translation, Coordinate3D up, int rotation)? Align(List<Coordinate3D> beacons1, List<Coordinate3D> beacons2)
        {
            foreach (var axis in _axes)
            {
                for (int rotation = 0; rotation < 4; rotation++)
                {
                    var rotatedBeacons2 = new List<Coordinate3D>(beacons2.Select(b => Transform(b, axis, rotation)));

                    foreach (var b1 in beacons1)
                    {
                        foreach (var matchingB1InB2 in rotatedBeacons2)
                        {
                            var delta = b1 - matchingB1InB2;
                            var transformedBeacons2 = new List<Coordinate3D>(rotatedBeacons2.Select(b => b + delta));

                            var intersection = new List<Coordinate3D>();
                            intersection = intersection.Union(transformedBeacons2).ToList();
                            intersection = intersection.Intersect(beacons1).ToList();
                            if (intersection.Count >= 12)
                            {
                                return (transformedBeacons2, delta, axis, rotation);
                            }
                        }
                    }
                }
            }
            return null;
        }

        private Coordinate3D Transform(Coordinate3D point, Coordinate3D up, int rotation)
        {
            Coordinate3D reoriented = up switch
            {
                (0, 1, 0) => point,
                (0, -1, 0) => (point.X, -point.Y, -point.Z),
                (1, 0, 0) => (point.Y, point.X, -point.Z),
                (-1, 0, 0) => (point.Y, -point.X, point.Z),
                (0, 0, 1) => (point.Y, point.Z, point.X),
                (0, 0, -1) => (point.Y, -point.Z, -point.X),
                _ => throw new Exception("Invalid up vector")
            };

            return rotation switch
            {
                0 => reoriented,
                1 => (reoriented.Z, reoriented.Y, -reoriented.X),
                2 => (-reoriented.X, reoriented.Y, -reoriented.Z),
                3 => (-reoriented.Z, reoriented.Y, reoriented.X),
                _ => throw new Exception("Invalid rotation")
            };
        }
    }
}
