using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode.Day7
{
    public static class DaySeven
    {
        public static string ExecuteFirst()
        {
            var count = 0;
            var colors = GetInput();

            foreach (var color in colors)
            {
                if (SearchForGold(color.Value, colors))
                {
                    count++;
                }
            }

            return count.ToString();
        }

        public static string ExecuteSecond()
        {
            var colors = GetInput();

            var bags = 0;
            foreach (var color in colors["shiny gold"])
            {
                bags += color.Value;
                bags += CountBags(colors[color.Key], colors) * color.Value;
            }

            return bags.ToString();
        }

        private static int CountBags(Dictionary<string, int> containingColors, Dictionary<string, Dictionary<string, int>> allColors)
        {
            int sum = 0;
            foreach (var color in containingColors)
            {
                sum += color.Value;
                sum += CountBags(allColors[color.Key], allColors) * color.Value;
            }

            return sum;
        }

        private static bool SearchForGold(Dictionary<string, int> containingColors, Dictionary<string, Dictionary<string, int>> allColors)
        {

            if (!containingColors.Any())
            {
                return false;
            }
            if (containingColors.Keys.Contains("shiny gold"))
            {
                return true;
            }

            foreach (var containingColor in containingColors)
            {
                if (SearchForGold(allColors[containingColor.Key], allColors))
                {
                    return true;
                }
            }

            return false;
        }

        private static Dictionary<string, Dictionary<string, int>> GetInput()
        {
            var colorCollection = new Dictionary<string, Dictionary<string, int>>();

            var fileStream = new FileStream("Day7/input.txt", FileMode.Open);
            using var reader = new StreamReader(fileStream);
            string input;
            while ((input = reader.ReadLine()) != null)
            {
                var values = input.TrimEnd('.').Split("contain");
                if (values[1].Trim() != "no other bags")
                {
                    var colors = values[1].Split(",").Select(colorValue => Sanitize(colorValue.Trim())).ToList();

                    Dictionary<string, int> dictionary = new Dictionary<string, int>();
                    foreach (var color in colors)
                    {
                        var strings = color.Split(' ', 2);
                        dictionary.Add(strings[1], int.Parse(strings[0]));
                    }

                    colorCollection.Add(Sanitize(values[0]), dictionary);
                }
                else
                {
                    colorCollection.Add(Sanitize(values[0]), new Dictionary<string, int>());
                }
            }
            return colorCollection;
        }

        private static string Sanitize(string input)
        {
            return input.Replace("bags", string.Empty).Replace("bag", string.Empty).Trim();
        }
    }
}
