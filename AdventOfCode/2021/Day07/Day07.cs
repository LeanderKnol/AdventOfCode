using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Instrumentation;

namespace AdventOfCode._2021.Day07
{
    public class Day07
    {
        private readonly List<int> _input;
        public Day07()
        {
            //_input = Helpers.ReadInputArray<string>("2021/Day07/ExampleInput.txt").First().Split(",").Select(i => int.Parse(i)).ToList();
            _input = Helpers.ReadInputArray<string>("2021/Day07/Input.txt").First().Split(",").Select(int.Parse).ToList();
        }

        public void Part1()
        {
            CalculateFuel(true);
        }

        private void CalculateFuel(bool isConstant)
        {
            Dictionary<int, int> positions = new();
            for (int currentPosition = _input.Min(); currentPosition <= _input.Max(); currentPosition++)
            {
                if ((isConstant && !_input.Contains(currentPosition)) || positions.ContainsKey(currentPosition)) continue;
                positions[currentPosition] = 0;
                foreach (int testPosition in _input)
                {
                    int moves = Math.Abs(testPosition - currentPosition);
                    positions[currentPosition] += isConstant ? moves : moves * (moves + 1) / 2; ;
                }
            }

            Console.WriteLine(positions.Values.Min());
        }

        public void Part2()
        {
            CalculateFuel(false);
        }
    }
}
