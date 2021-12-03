using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode._2020.Day19
{
    public static class DayNineteen
    {
        public static string ExecuteFirst()
        {

            var input = GetInputArray();
            var processedRules = new Dictionary<string, string>();

            var regex = new Regex($"^{ProcessRule("0", input.Rules, processedRules)}$");
            return input.Messages.Count(regex.IsMatch).ToString();
        }



        public static string ExecuteSecond()
        {
            var input = GetInputArray();
            var processedRules = new Dictionary<string, string>();

            var regex = new Regex($@"^({ProcessRule("42", input.Rules, processedRules)})+(?<open>{ProcessRule("42", input.Rules, processedRules)})+(?<close-open>{ProcessRule("31", input.Rules, processedRules)})+(?(open)(?!))$");
            return input.Messages.Count(regex.IsMatch).ToString();
        }



        private static Input GetInputArray()
        {
            var result = new Input();

            FileStream fileStream = new FileStream("Day19/input.txt", FileMode.Open);
            using StreamReader reader = new StreamReader(fileStream);

            string input;
            while (!string.IsNullOrWhiteSpace(input = reader.ReadLine()))
            {
                var parts = input.Split(':');
                result.Rules.Add(parts[0], parts[1].Replace("\"", string.Empty).Trim());
            }

            while ((input = reader.ReadLine()) != null)
            {
                result.Messages.Add(input);
            }


            return result;
        }

        private static string ProcessRule(string ruleNumber, Dictionary<string, string> originalRules,
            Dictionary<string, string> processedRules)
        {
            if (processedRules.ContainsKey(ruleNumber))
            {
                return processedRules[ruleNumber];
            }

            var originalRule = originalRules[ruleNumber];
            if (!originalRule.Any(char.IsDigit))
            {
                return processedRules[ruleNumber] = originalRule;
            }

            if (!originalRule.Contains("|"))
            {
                return processedRules[ruleNumber] =
                    string.Join(string.Empty, originalRule.Split(' ').Select(x => ProcessRule(x, originalRules, processedRules)));
            }

            return processedRules[ruleNumber] =
                $"({string.Join(string.Empty, originalRule.Split(' ').Select(x => x == "|" ? x : ProcessRule(x, originalRules, processedRules)))})";
        }
    }
}
