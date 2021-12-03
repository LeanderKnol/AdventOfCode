using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace AdventOfCode._2020.Day2
{
    public static class DayTwo
    {
        public static string ExecuteFirst()
        {
            var input = GetInput();
            var validPasswords = 0;
            foreach (var corruptedPasswordLine in input)
            {
                var matches = Regex.Matches(corruptedPasswordLine.Password, corruptedPasswordLine.SearchCharacter);
                if (matches.Count >= corruptedPasswordLine.Minimum && matches.Count <= corruptedPasswordLine.Maximum)
                {
                    validPasswords++;
                }

            }
            return validPasswords.ToString();
        }

        public static string ExecuteSecond()
        {
            var input = GetInput();
            var validPasswords = 0;
            foreach (var corruptedPasswordLine in input)
            {
                var firstMatch = corruptedPasswordLine.Password[corruptedPasswordLine.Minimum - 1].ToString() == corruptedPasswordLine.SearchCharacter;
                var secondMatch = corruptedPasswordLine.Password[corruptedPasswordLine.Maximum - 1].ToString() == corruptedPasswordLine.SearchCharacter;
                if ((firstMatch && !secondMatch) || (!firstMatch && secondMatch))
                {
                    validPasswords++;
                }

            }
            return validPasswords.ToString();
        }

        private static List<CorruptedPasswordLine> GetInput()
        {
            var corruptedPasswordLines = new List<CorruptedPasswordLine>();

            var fileStream = new FileStream("Day2/input.txt", FileMode.Open);
            using var reader = new StreamReader(fileStream);
            string input;
            while ((input = reader.ReadLine()) != null)
            {
                var splitInput = input.Replace(":", string.Empty).Split(' ');
                if (splitInput.Length == 3)
                {
                    var splitLimits = splitInput[0].Split('-');
                    if (splitLimits.Length == 2 && int.TryParse(splitLimits[0], out var minimum) && int.TryParse(splitLimits[1], out var maximum))
                    {
                        corruptedPasswordLines.Add(new CorruptedPasswordLine
                        {
                            Password = splitInput[2],
                            SearchCharacter = splitInput[1],
                            Minimum = minimum,
                            Maximum = maximum
                        });
                    }
                }
            }

            return corruptedPasswordLines;
        }
    }
}
