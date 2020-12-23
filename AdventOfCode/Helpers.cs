using System;

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
    }
}
