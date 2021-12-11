using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Instrumentation;

namespace AdventOfCode._2021.Day7
{
    public class Day7
    {
        private readonly List<int> _input;
        public Day7()
        {
            //_input = Helpers.ReadInputArray<string>("2021/Day7/ExampleInput.txt").First().Split(",").Select(i => int.Parse(i)).ToList();
            _input = Helpers.ReadInputArray<string>("2021/Day7/Input.txt").First().Split(",").Select(i => int.Parse(i)).ToList();
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
