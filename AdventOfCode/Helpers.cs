using System;
using System.Collections.Generic;
using System.Text;

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

        public static string GetSide(this List<string> grid, int xStart, int xEnd, int yStart, int yEnd)
        {
            var result = new StringBuilder();
            for (var i = xStart; i <= xEnd; i++)
            {
                for (var j = yStart; j <= yEnd; j++)
                {
                    result.Append(grid[i][j]);
                }
            }
            return result.ToString();
        }

        public static List<string> Rotate(this List<string> source, int length)
        {

            var rotated = new List<string>();

            for (var y = 0; y < length; y++)
            {
                var sb = new StringBuilder();
                for (var x = 0; x < length; x++)
                {
                    sb.Append(source[length - x - 1][y]);
                }
                rotated[y] = sb.ToString();
            }

            return rotated;
        }
    }
}
