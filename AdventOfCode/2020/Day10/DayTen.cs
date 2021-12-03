using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode._2020.Day10
{
    public static class DayTen
    {
        public static string ExecuteFirst()
        {
            var numbers = GetInput();
            var diffThree = 1;
            var diffOne = 0;
            var jolt = 0;

            for (int i = 0; i < numbers.Count; i++)
            {
                if (numbers[i] - jolt == 1)
                {
                    diffOne++;
                }
                if (numbers[i] - jolt == 3)
                {
                    diffThree++;
                }

                jolt = numbers[i];
            }

            return (diffOne * diffThree).ToString();
        }


        public static string ExecuteSecond()
        {
            var numbers = GetInput();

            numbers.Add(numbers.Max() + 3);
            numbers.Add(0);

            numbers.Sort();

            var connections = new Dictionary<int, long> { [numbers.Count() - 1] = 1 };

            for (var i = numbers.Count() - 2; i >= 0; i--)
            {
                long currentCount = 0;
                for (var connected = i + 1; connected < numbers.Count() && numbers[connected] - numbers[i] <= 3; connected++)
                {
                    currentCount += connections[connected];
                }
                connections[i] = currentCount;
            }

            return connections[0].ToString();
        }

        private static List<int> GetInput()
        {
            var numbers = new List<int>();

            var fileStream = new FileStream("Day10/input.txt", FileMode.Open);
            using var reader = new StreamReader(fileStream);
            string input;
            while ((input = reader.ReadLine()) != null)
            {
                numbers.Add(int.Parse(input));

            }

            numbers.Sort();

            return numbers;
        }
    }
}
