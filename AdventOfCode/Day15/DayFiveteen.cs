using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Day15
{
    public static class DayFifteen
    {
        public static string ExecuteFirst()
        {
            var numbers = CountOptimized(2020);
            return numbers.ToString();
        }

        public static string ExecuteSecond()
        {
            var numbers = CountOptimized(30000000);
            return numbers.ToString();
        }


        private static List<int> Count(int amount)
        {
            var numbers = new List<int> { 1, 12, 0, 20, 8, 16 };

            while (numbers.Count < amount)
            {
                var lastNumber = numbers.Last();
                var lastIndex = numbers.LastIndexOf(lastNumber, numbers.Count - 2);
                if (lastIndex == -1)
                {
                    numbers.Add(0);
                }
                else
                {
                    numbers.Add(numbers.Count - (lastIndex + 1));
                }
            }

            return numbers;
        }

        private static List<int> CountReversed(int amount)
        {
            var numbers = new List<int> { 16, 8, 20, 0, 12, 1 };
            while (numbers.Count < amount)
            {
                var lastNumber = numbers.First();
                var lastIndex = numbers.IndexOf(lastNumber, 1);
                numbers.Insert(0, lastIndex == -1 ? 0 : lastIndex);
            }

            return numbers;
        }

        private static int CountOptimized(int amount)
        {
            var startingNumbers = new List<int> { 1, 12, 0, 20, 8, 16 };

            var spokenNumbers = new int[amount];

            for (var i = 0; i < startingNumbers.Count - 1; i++)
            {
                spokenNumbers[startingNumbers[i]] = i + 1;
            }

            var lastNumber = startingNumbers[^1];

            for (var i = startingNumbers.Count - 1; i < amount - 1; i++)
            {
                var spokenNumber = spokenNumbers[lastNumber];
                spokenNumbers[lastNumber] = i + 1;
                lastNumber = spokenNumber == 0 ? 0 : i + 1 - spokenNumber;
            }

            return lastNumber;
        }
    }
}
