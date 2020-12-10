using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode.Day4
{
    public static class DayFour
    {
        public static string ExecuteFirst()
        {
            var validPassports = GetValidFieldPassports();

            return validPassports.Count().ToString();
        }

        public static string ExecuteSecond()
        {
            var validFieldPassports = GetValidFieldPassports();
            var validPassports = 0;

            foreach (var passport in validFieldPassports)
            {

                if (
                    IntBetween(passport["byr"], 1920, 2002) &&
                    IntBetween(passport["iyr"], 2010, 2020) &&
                    IntBetween(passport["eyr"], 2020, 2030) &&
                    Regex.IsMatch(passport["pid"], @"^\d{9}$") &&
                    Regex.IsMatch(passport["hcl"], @"^#[0-9a-fA-F]{6}$") &&
                    ((passport["hgt"].Contains("cm") && IntBetween(passport["hgt"].Replace("cm", string.Empty), 150, 193))
                     || (passport["hgt"].Contains("in") && IntBetween(passport["hgt"].Replace("in", string.Empty), 59, 76)))
                   && Regex.Matches(passport["ecl"], @"(amb|blu|brn|gry|grn|hzl|oth)").Count == 1
                    )
                {
                    validPassports++;
                }

            }

            return validPassports.ToString();
        }

        private static bool IntBetween(string value, int min, int max)
        {
            var intValue = int.Parse(value);
            return intValue >= min && intValue <= max;
        }

        private static List<Dictionary<string, string>> GetValidFieldPassports()
        {
            string[] requiredFields = { "byr", "iyr", "eyr", "hgt", "hcl", "ecl", "pid" };
            var input = GetInput();
            var validPassports = new List<Dictionary<string, string>>();

            foreach (var passport in input)
            {
                if (requiredFields.All(requiredField => passport.ContainsKey(requiredField)))
                {
                    validPassports.Add(passport);
                }
            }

            return validPassports;
        }

        private static List<Dictionary<string, string>> GetInput()
        {
            var passports = new List<Dictionary<string, string>>();
            var passport = new Dictionary<string, string>();

            var fileStream = new FileStream("Day4/input.txt", FileMode.Open);
            using var reader = new StreamReader(fileStream);
            string input;
            while ((input = reader.ReadLine()) != null)
            {
                if (string.IsNullOrWhiteSpace(input))
                {
                    if (passport.Count >= 7)
                    {
                        passports.Add(passport);
                    }

                    passport = new Dictionary<string, string>();
                }
                else
                {
                    var fields = input.Split(' ');
                    foreach (var field in fields)
                    {
                        var splitField = field.Split(':');
                        passport.Add(splitField[0], splitField[1].Trim());
                    }
                }
            }

            if (passport.Count >= 7)
            {
                passports.Add(passport);
            }

            return passports;
        }
    }
}
