using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode._2020.Day21
{
    public static class DayTwentyOne
    {
        public static string ExecuteFirst()
        {
            var input = GetInputArray();

            var allergenMatches = GetAllergenMatches(input);

            var allergens = allergenMatches.SelectMany(am => am.Value).ToList();
            return input.SelectMany(l => l.Ingredients).Count(i => !allergens.Contains(i)).ToString();
        }

        private static Dictionary<string, List<string>> GetAllergenMatches(List<Label> input)
        {
            var allergenMatches = new Dictionary<string, List<string>>();
            foreach (var label in input)
            {
                foreach (var allergen in label.Allergen)
                {
                    if (!allergenMatches.ContainsKey(allergen))
                    {
                        allergenMatches.Add(allergen, label.Ingredients);
                    }
                    else
                    {
                        allergenMatches[allergen] =
                            allergenMatches[allergen].Select(s => s).Intersect(label.Ingredients).ToList();
                    }
                }
            }

            while (allergenMatches.Any(am => am.Value.Count > 1))
            {
                var singleValues = allergenMatches.Where(v => v.Value.Count == 1).SelectMany(v => v.Value);
                foreach (var value in allergenMatches.Where(v => v.Value.Count > 1))
                {
                    value.Value.RemoveAll(v => singleValues.Contains(v));
                }
            }

            return allergenMatches;
        }

        public static string ExecuteSecond()
        {
            var input = GetInputArray();
            var allergenMatches = GetAllergenMatches(input)
                .OrderBy(am => am.Key);
            var allergens = allergenMatches.SelectMany(am => am.Value).ToList();
            return string.Join(',', allergens);
        }

        private static List<Label> GetInputArray()
        {
            FileStream fileStream = new FileStream("Day21/input.txt", FileMode.Open);
            using StreamReader reader = new StreamReader(fileStream);

            var result = new List<Label>();

            string input;
            while ((input = reader.ReadLine()) != null)
            {
                var label = new Label();
                var components = input.Split('(', StringSplitOptions.TrimEntries);
                label.Ingredients = components[0].Split(' ').ToList();
                label.Allergen = components[1].Replace("contains", string.Empty).Replace(")", string.Empty)
                    .Split(',', StringSplitOptions.TrimEntries).ToList();

                result.Add(label);
            }
            return result;
        }


    }
}
