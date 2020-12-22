using System;
using System.Collections.Generic;

namespace AdventOfCode.Day20
{
    public class Tile
    {
        public int Number { get; set; }
        public List<string> Image { get; set; } = new List<string>();

        public string Top => Image.GetSide(0, 9, 0, 0);
        public string Bottom => Image.GetSide(0, 9, 0, 0);
        public string Left => Image.GetSide(0, 0, 0, 9);
        public string Right => Image.GetSide(9, 9, 0, 9);


        public void Rotate()
        {
            Image.Rotate(10);
        }

        public new List<string> GetSides()
        {
            var sides = new List<string>();

            sides.Add(Top);
            sides.Add(Reverse(Top));

            sides.Add(Bottom);
            sides.Add(Reverse(Bottom));

            sides.Add(Left);
            sides.Add(Reverse(Left));

            sides.Add(Right);
            sides.Add(Reverse(Right));


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
