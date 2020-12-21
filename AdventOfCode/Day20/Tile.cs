using System;
using System.Collections.Generic;

namespace AdventOfCode.Day20
{
    public class Tile
    {
        public int Number { get; set; }
        public List<string> Image { get; set; } = new List<string>();

        public new List<string> GetSides()
        {
            var sides = new List<string>();

            var top = Image.GetSide(0, 9, 0, 0);
            sides.Add(top);
            sides.Add(Reverse(top));

            var bottom = Image.GetSide(0, 9, 9, 9);
            sides.Add(bottom);
            sides.Add(Reverse(bottom));

            var left = Image.GetSide(0, 0, 0, 9);
            sides.Add(left);
            sides.Add(Reverse(left));

            var right = Image.GetSide(9, 9, 0, 9);
            sides.Add(right);
            sides.Add(Reverse(right));


            string Reverse(string input)
            {
                char[] array = input.ToCharArray();
                Array.Reverse(array);
                return new string(array);
            }

            return sides;
        }
    }
}
