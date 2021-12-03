using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;

namespace AdventOfCode
{
    public static class Helpers
    {
        public static string ToBinaryString(this int number, int length)
        {
            var bin = Convert.ToString(number, 2);
            return bin.PadLeft(length, '0');
        }

        public static string ToBinaryString(this long number, int length)
        {
            var bin = Convert.ToString(number, 2);
            return bin.PadLeft(length, '0');
        }

        public static string ReverseString(this string text)
        {
            char[] array = text.ToCharArray();
            Array.Reverse(array);
            return new string(array);
        }

        public static long BinaryToNumber(this string binaryString)
        {
            return Convert.ToInt64(binaryString, 2);
        }

        public static T[] ReadInputArray<T>(string path)
        {
            var inputs = new List<T>();

            FileStream fileStream = new FileStream(path, FileMode.Open);
            using StreamReader reader = new StreamReader(fileStream);

            string input;
            while ((input = reader.ReadLine()) != null)
            {
                var convertedInput = TypeDescriptor.GetConverter(typeof(T)).ConvertFromString(input);
                inputs.Add((T)convertedInput);
            }

            var inputArray = inputs.ToArray();
            return inputArray;
        }
    }
}
