using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode._2021.Day6
{
    public class Day6
    {
        private List<int> _input;
        private long[] _days;
        public Day6()
        {
            //_input = Helpers.ReadInputArray<string>("2021/Day6/ExampleInput.txt").First().Split(",").Select(i => int.Parse(i)).ToList();
            _input = Helpers.ReadInputArray<string>("2021/Day6/Input.txt").First().Split(",").Select(i => int.Parse(i)).ToList();
        }

        public void Part1()
        {
            _days = new long[9];
            foreach (int item in _input)
            {
                _days[item]++;
            }
            CountFishBetter(80);
        }

        public void Part2()
        {
            _days = new long[9];
            foreach (int item in _input)
            {
                _days[item]++;
            }

            CountFishBetter(256);
        }

        private void CountFish(int days)
        {
            for (int i = 0; i < days; i++)
            {
                var newInput = _input.Select(x => x).ToList();
                ;
                for (int j = 0; j < _input.Count(); j++)
                {
                    if (_input[j] == 0)
                    {
                        newInput.Add(8);
                        newInput[j] = 6;
                    }
                    else
                    {
                        newInput[j] = _input[j] - 1;
                    }
                }

                _input = newInput;
            }

            Console.WriteLine(_input.Count());
        }

        private void CountFishBetter(int days)
        {
            for (int i = 0; i < days; i++)
            {
                var breeding = _days[0];

                for (int j = 1; j <= 8; j++)
                {
                    _days[j - 1] = _days[j];
                }

                _days[6] += breeding;
                _days[8] = breeding;
            }
            Console.WriteLine(_days.Sum());

        }
    }
}
