using System;
using AdventOfCode.Instrumentation;

namespace AdventOfCode._2021.Day1
{
    public class Day1
    {
        private int[] input;
        public Day1()
        {
            input = Helpers.ReadInputArray<int>("2021/Day1/Input.txt");
            //input = Helpers.ReadInputArray<int>("2021/Day1/ExampleInput.txt");
        }

        public void Part1()
        {
            int increased = 0;
            for (int i = 1; i < input.Length; i++)
            {
                if (input[i] > input[i-1])
                {
                    increased++;
                }
            }
            Console.WriteLine(increased);
        }

        public void Part2()
        {
            int increased = 0;
            for (int i = 1; i < input.Length-2; i++)
            {
                var current = input[i] + input[i + 1] + input[i + 2];
                var previous = input[i-1] + input[i] + input[i + 1];

                if (current > previous)
                {
                    increased++;
                }
            }
            Console.WriteLine(increased);
        }
    }
}
