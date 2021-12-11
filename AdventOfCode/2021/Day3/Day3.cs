using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Instrumentation;

namespace AdventOfCode._2021.Day3
{
    public class Day3
    {
        private readonly List<string> _input;
        public Day3()
        {
            _input = Helpers.ReadInputArray<string>("2021/Day3/Input.txt").ToList();
            //_input = Helpers.ReadInputArray<string>("2021/Day3/ExampleInput.txt").ToList();
        }

        public void Part1()
        {
            var mostCommonString = string.Empty;
            var leastCommonString = string.Empty;

            for (int i = 0; i < _input.First().Length; i++)
            {
                var digits = _input.Select(val => val[i..(i + 1)]);
                var ones = digits.Count(d => d == "1");
                var zeroes = digits.Count(d => d == "0");

                if (ones > zeroes)
                {
                    mostCommonString += "1";
                    leastCommonString += "0";
                }
                else
                {
                    mostCommonString += "0";
                    leastCommonString += "1";
                }
            }

            Console.WriteLine(mostCommonString.BinaryToNumber() * leastCommonString.BinaryToNumber());
        }

        public void Part2()
        {
            var mostInput = _input;

            int i = 0;
            while (true)
            {
                var digits = mostInput.Select(val => val[i..(i + 1)]);

                var ones = digits.Count(d => d == "1");
                var zeroes = digits.Count(d => d == "0");

                mostInput = mostInput.Where(val => val[i..(i + 1)] == (ones >= zeroes ? "1" : "0")).ToList();

                if (mostInput.Count() == 1)
                {
                    break;
                }
                i++;
            }

            var leastInput = _input;
            i = 0;
            while (true)
            {
                var digits = leastInput.Select(val => val[i..(i + 1)]);

                var ones = digits.Count(d => d == "1");
                var zeroes = digits.Count(d => d == "0");

                leastInput = leastInput.Where(val => val[i..(i + 1)] == (ones >= zeroes ? "0" : "1")).ToList();

                if (leastInput.Count() == 1)
                {
                    break;
                }
                i++;
            }


            Console.WriteLine(mostInput.First().BinaryToNumber() * leastInput.First().BinaryToNumber());
        }
    }
}
