using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode._2020.Day1
{
    public static class DayOne
    {
        public static string ExecuteFirst()
        {
            var inputArray = GetInputArray();

            for (var i1 = 0; i1 < inputArray.Length; i1++)
            {
                for (var i2 = 0; i2 < inputArray.Length; i2++)
                {
                    if (i1 != i2 && (inputArray[i1] + inputArray[i2]) == 2020)
                    {
                        return (inputArray[i1] * inputArray[i2]).ToString();

                    }
                }

            }

            return string.Empty;
        }

        public static string ExecuteSecond()
        {
            var inputArray = GetInputArray();

            for (var i1 = 0; i1 < inputArray.Length; i1++)
            {
                for (var i2 = 0; i2 < inputArray.Length; i2++)
                {
                    for (var i3 = 0; i3 < inputArray.Length; i3++)
                    {
                        if ((new[] { i1, i2, i3 }.All(x => x != 1)) && (inputArray[i1] + inputArray[i2] + inputArray[i3]) == 2020)
                        {
                            return (inputArray[i1] * inputArray[i2] * inputArray[i3]).ToString();

                        }
                    }
                }
            }

            return string.Empty;
        }

        private static int[] GetInputArray()
        {
            var inputs = new List<int>();

            FileStream fileStream = new FileStream("Day1/input.txt", FileMode.Open);
            using StreamReader reader = new StreamReader(fileStream);

            string input;
            while ((input = reader.ReadLine()) != null)
            {
                if (int.TryParse(input, out var intInput))
                {
                    inputs.Add(intInput);
                }
            }

            var inputArray = inputs.ToArray();
            return inputArray;
        }
    }
}
