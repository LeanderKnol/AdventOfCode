using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode._2020.Day6
{
    public static class DaySix
    {
        public static string ExecuteFirst()
        {
            var count = 0;
            foreach (var answers in GetInput())
            {
                count += answers.Distinct().Count();
            }
            return count.ToString();
        }

        public static string ExecuteSecond()
        {
            var count = 0;
            var matching = new List<char>();
            var isNewList = true;

            var fileStream = new FileStream("Day6/input.txt", FileMode.Open);
            using var reader = new StreamReader(fileStream);
            string input;
            while ((input = reader.ReadLine()) != null)
            {
                if (!string.IsNullOrWhiteSpace(input))
                {
                    if (isNewList)
                    {
                        matching.AddRange(input.ToCharArray());
                        isNewList = false;
                    }
                    else
                    {
                        matching = matching.Intersect(input.ToCharArray()).ToList();
                    }
                }
                else
                {
                    count += matching.Count;
                    matching = new List<char>();
                    isNewList = true;
                }
            }
            count += matching.Count;

            return count.ToString();
        }

        private static List<List<char>> GetInput()
        {
            var answerCollection = new List<List<char>>();
            var answer = new List<char>();

            var fileStream = new FileStream("Day6/input.txt", FileMode.Open);
            using var reader = new StreamReader(fileStream);
            string input;
            while ((input = reader.ReadLine()) != null)
            {
                if (string.IsNullOrWhiteSpace(input))
                {
                    answerCollection.Add(answer);
                    answer = new List<char>();
                }
                else
                {
                    answer.AddRange(input.ToCharArray());
                }
            }
            answerCollection.Add(answer);
            return answerCollection;
        }
    }
}
