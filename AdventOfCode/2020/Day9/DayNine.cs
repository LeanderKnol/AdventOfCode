using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode._2020.Day9
{
    public static class DayNine
    {
        public static string ExecuteFirst()
        {
            var numbers = GetInput();

            return FindWeekness(numbers).ToString();
        }

        private static long FindWeekness(List<long> numbers)
        {
            var preamble = 25;
            for (int i = preamble; i < numbers.Count; i++)
            {
                var checkRange = numbers.Skip(i - preamble).Take(preamble).ToList();
                var numberToCheck = numbers[i];

                var matchFound = (from item1 in checkRange
                                  from item2 in checkRange
                                  where item1 != item2 && item1 + item2 == numberToCheck
                                  select item1).Any();

                if (!matchFound)
                {
                    return numberToCheck;
                }
            }

            return 0;
        }


        public static string ExecuteSecond()
        {
            var numbers = GetInput();

            var weakness = FindWeekness(numbers);
            for (int i = 0; i < numbers.Count; i++)
            {
                var total = 0L;
                var items = new List<long>();

                foreach (var number in numbers.Skip(i))
                {
                    if (total < weakness)
                    {
                        total += number;
                        items.Add(number);
                    }
                    else if (total == weakness)
                    {
                        return (items.Min() + items.Max()).ToString();
                    }
                    else
                    {
                        break;
                    }
                }
            }


            return weakness.ToString();
        }


        private static List<long> GetInput()
        {
            var instructions = new List<long>();

            var fileStream = new FileStream("Day9/input.txt", FileMode.Open);
            using var reader = new StreamReader(fileStream);
            string input;
            while ((input = reader.ReadLine()) != null)
            {
                instructions.Add(long.Parse(input));

            }
            return instructions;
        }
    }
}
