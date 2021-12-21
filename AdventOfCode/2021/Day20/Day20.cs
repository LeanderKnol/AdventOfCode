using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Instrumentation;
using AdventOfCode.Instrumentation.DataTypes;

namespace AdventOfCode._2021.Day20
{
    public class Day20
    {
        private readonly string _algorithm;
        private readonly Dictionary<Coordinate2D,int> _image = new();
        private Dictionary<Coordinate2D, int> _processingImage;
        private int _iterations;
        private int _minX = -1;
        private int _minY = -1;
        private int _maxX;
        private int _maxY;


        public Day20()
        {
            //var input = Helpers.ReadInputArray<string>("2021/Day20/ExampleInput.txt");
            var input = Helpers.ReadInputArray<string>("2021/Day20/Input.txt");
            _algorithm = string.Empty;
            foreach (var character in input[0])
            {
                _algorithm += ConvertToBinary(character);
            }

            var image = input.Skip(2).ToList();
            for (int y = 0; y < image.Count; y++)
            {
                for (int x = 0; x < image[y].Length; x++)
                {
                    _image.Add((x,y), ConvertToBinary(image[y][x]));
                }
            }

            _maxY = image.Count +1;
            _maxX = image[2].Length + 1;
        }

        private Dictionary<Coordinate2D, int> Process(Dictionary<Coordinate2D, int> image)
        {
            var newImage = new Dictionary<Coordinate2D, int>();
            for (int y = _minY; y < _maxY ; y++)
            {
                for (int x = _minX; x < _maxX; x++)
                {
                    newImage.Add((x,y), GetPixelValue(image, (x, y)));
                }
            }

            _minX--;
            _minY--;
            _maxX++;
            _maxY++;
            _iterations++;

            Print();
            return newImage;
        }

        private int GetPixelValue(Dictionary<Coordinate2D, int> image, Coordinate2D coordinate)
        {
            var binaryString = string.Empty;
            foreach (var neighbor in Coordinate2D.GetTotalSquare())
            {
                var actual = coordinate + neighbor;
                if (_iterations % 2 == 0)
                { 
                    if (image.ContainsKey(actual))
                    {
                        if (image[actual] == 1)
                        {
                            binaryString += '1';
                        }
                        else
                        {
                            binaryString += '0';
                        }
                    }
                    else
                    {
                        binaryString += '0';
                    }
                }
                else
                {
                    if (image.ContainsKey(actual))
                    {
                        if (image[actual] == 1)
                        {
                            binaryString += '1';
                        }
                        else
                        {
                            binaryString += '0';
                        }
                    }
                    else
                    {
                        binaryString += '1';
                    }
                }
            }
            var index = Convert.ToInt32(binaryString, 2);
            return _algorithm[index].ToInt();
        }

        private int ConvertToBinary(char c)
        {
            return c == '.' ? 0 : 1;
        }

        private void Print()
        {
            return;
            var minX = _processingImage.Min(o => o.Key.X);
            var minY = _processingImage.Min(o => o.Key.Y);

            var maxX = _processingImage.Max(o => o.Key.X);
            var maxY = _processingImage.Max(o => o.Key.Y);

            for (int y = minY; y < maxY; y++)
            {
                for (int x = minX; x < maxX; x++)
                {
                    Console.Write(_processingImage[(x, y)] == 1 ? "#" : ".");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        public void Part1()
        {
            _processingImage = _image;

            while (_iterations < 2)
            {
                _processingImage = Process(_processingImage);
            }
          
            Console.WriteLine(_processingImage.Count(pixel => pixel.Value == 1));
        }

        public void Part2()
        {
            while (_iterations < 50)
            {
                _processingImage = Process(_processingImage);
            }

            Console.WriteLine(_processingImage.Count(pixel => pixel.Value == 1));
        }
    }
}
