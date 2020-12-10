using System.Collections.Generic;
using System.IO;

namespace AdventOfCode.Day3
{
    public static class DayThree
    {
        public static string ExecuteFirst()
        {
            var input = GetInput();
            return CheckEncounters(input, 1, 3).ToString();
        }
        public static string ExecuteSecond()
        {
            var input = GetInput();
            return (CheckEncounters(input, 1, 1) *
                    CheckEncounters(input, 1, 3) *
                    CheckEncounters(input, 1, 5) *
                    CheckEncounters(input, 1, 7) *
                    CheckEncounters(input, 2, 1)
                     ).ToString();
        }


        private static long CheckEncounters(char[][] input, int stepsDown, int stepsRight)
        {
            long trees = 0;
            var rowNr = 0;
            var colNr = 0;

            while (rowNr < input.Length)
            {
                if (input[rowNr][colNr] == '#')
                {
                    trees++;
                }

                rowNr += stepsDown;
                colNr += stepsRight;

                if (rowNr < input.Length && colNr >= input[rowNr].Length)
                {
                    colNr -= input[rowNr].Length;
                }
            }

            return trees;
        }

        private static char[][] GetInput()
        {
            var fileStream = new FileStream("Day3/input.txt", FileMode.Open);
            using var reader = new StreamReader(fileStream);
            string input;
            var rows = new List<char[]>();
            while ((input = reader.ReadLine()) != null)
            {
                rows.Add(input.ToCharArray());
            }

            return rows.ToArray();
        }
    }
}
