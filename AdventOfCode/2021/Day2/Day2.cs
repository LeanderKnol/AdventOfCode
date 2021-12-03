﻿using System;
using System.Collections.Generic;

namespace AdventOfCode._2021.Day2
{
    public class Day2
    {
        private readonly List<KeyValuePair<string, int>> _instructions = new List<KeyValuePair<string, int>>();
        public Day2()
        {
            var input = Helpers.ReadInputArray<string>("2021/Day2/Input.txt");
            //var input = Helpers.ReadInputArray<string>("2021/Day2/ExampleInput.txt");
            foreach (var value in input)
            {
                var components = value.Split(" ");
                _instructions.Add(new KeyValuePair<string,int>(components[0], int.Parse(components[1])));
            }
        }

        public void Part1()
        {
            int horizontalPosition = 0;
            int depth = 0;

            foreach (var instruction in _instructions)
            {
                switch (instruction.Key)
                {
                    case "forward":
                        horizontalPosition = horizontalPosition + instruction.Value;
                        break;
                    case "down":
                        depth += instruction.Value;
                        break;
                    case "up":
                        depth -= instruction.Value;
                        break;
                }
            }
            Console.WriteLine(horizontalPosition* depth);
        }

        public void Part2()
        {
            int horizontalPosition = 0;
            int depth = 0;
            int aim = 0;

            foreach (var instruction in _instructions)
            {
                switch (instruction.Key)
                {
                    case "forward":
                        horizontalPosition += instruction.Value;
                        depth += (aim * instruction.Value);
                        break;
                    case "down":
                        aim += instruction.Value;
                        break;
                    case "up":
                        aim -= instruction.Value;

                        break;
                }
            }
            Console.WriteLine(horizontalPosition * depth);
        }
    }
}
