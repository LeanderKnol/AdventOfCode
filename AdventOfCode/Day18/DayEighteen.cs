using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AdventOfCode.Day18
{
    public static class DayEighteen
    {
        public static string ExecuteFirst()
        {

            var input = GetInputArray();

            var total = 0L;
            foreach (var calculation in input)
            {
                var parenthesesIndex = new List<int>();
                var newCalculation = new StringBuilder();

                foreach (var c in calculation)
                {
                    switch (c)
                    {
                        case '(':
                            newCalculation.Append(c);
                            parenthesesIndex.Add(newCalculation.Length);
                            break;
                        case ')':
                            var smallCalculation = newCalculation.ToString()[parenthesesIndex.Last()..];
                            var sum = CalculateBasic(smallCalculation);
                            newCalculation.Remove(parenthesesIndex.Last() - 1, newCalculation.Length - (parenthesesIndex.Last() - 1));
                            newCalculation.Append(sum.ToString());
                            parenthesesIndex.RemoveAt(parenthesesIndex.Count() - 1);
                            break;
                        default:
                            newCalculation.Append(c);
                            break;
                    }
                }
                total += CalculateBasic(newCalculation.ToString());

            }
            return total.ToString();
        }

        public static string ExecuteSecond()
        {

            var input = GetInputArray();

            var total = 0L;
            foreach (var calculation in input)
            {
                var parenthesesIndex = new List<int>();
                var newCalculation = new StringBuilder();

                foreach (var c in calculation)
                {
                    switch (c)
                    {
                        case '(':
                            newCalculation.Append(c);
                            parenthesesIndex.Add(newCalculation.Length);
                            break;
                        case ')':
                            var smallCalculation = newCalculation.ToString()[parenthesesIndex.Last()..];
                            var result = CalculateOrdered(smallCalculation);
                            newCalculation.Remove(parenthesesIndex.Last() - 1, newCalculation.Length - (parenthesesIndex.Last() - 1));
                            newCalculation.Append(result.ToString());
                            parenthesesIndex.RemoveAt(parenthesesIndex.Count() - 1);
                            break;
                        default:
                            newCalculation.Append(c);
                            break;
                    }
                }
                total += CalculateOrdered(newCalculation.ToString());

            }
            return total.ToString();
        }

        public static long CalculateBasic(string calculation)
        {

            var characters = calculation.Split(' ');
            var result = long.Parse(characters[0].ToString());

            for (int i = 1; i < characters.Length; i += 2)
            {
                switch (characters[i])
                {
                    case "+":
                        result += long.Parse(characters[i + 1].ToString());
                        break;
                    case "*":
                        result *= long.Parse(characters[i + 1].ToString());
                        break;
                }
            }
            return result;
        }


        public static long CalculateOrdered(string calculation)
        {
            var characters = calculation.Split(' ');
            var newCalculation = new StringBuilder();
            var previousAddValue = string.Empty;

            for (var i = 0; i < characters.Length; i++)
            {
                switch (characters[i])
                {
                    case "+":
                        var valueToAddTo = !string.IsNullOrEmpty(previousAddValue) ? previousAddValue : characters[i - 1];
                        var result = CalculateBasic($"{valueToAddTo} + {characters[i + 1]}").ToString();
                        previousAddValue = result;
                        newCalculation.Remove(newCalculation.Length - valueToAddTo.Length - 1, valueToAddTo.Length + 1);
                        newCalculation.Append(result);
                        newCalculation.Append(" ");
                        i += 1;
                        break;
                    default:
                        newCalculation.Append(characters[i]);
                        newCalculation.Append(" ");
                        previousAddValue = string.Empty;
                        break;
                }
            }
            return CalculateBasic(newCalculation.ToString());
        }

        private static List<string> GetInputArray()
        {
            var inputs = new List<string>();

            FileStream fileStream = new FileStream("Day18/input.txt", FileMode.Open);
            using StreamReader reader = new StreamReader(fileStream);

            string input;
            while ((input = reader.ReadLine()) != null)
            {
                inputs.Add(input);
            }

            return inputs;
        }
    }
}
