﻿using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode.Day20
{
    public class Tile
    {
        public const int TileSize = 10;

        public int Number { get; set; }
        public List<string> Image { get; set; } = new List<string>();

        public string Top => Image.GetSide(0, 9, 0, 0);
        public string Bottom => Image.GetSide(0, 9, 9, 9);
        public string Left => Image.GetSide(0, 0, 0, 9);
        public string Right => Image.GetSide(9, 9, 0, 9);

        public List<string> GetSides()
        {
            var sides = new List<string>
            {
                Top,
                Reverse(Top),
                Bottom,
                Reverse(Bottom),
                Left,
                Reverse(Left),
                Right,
                Reverse(Right)
            };


            static string Reverse(string input)
            {
                char[] array = input.ToCharArray();
                Array.Reverse(array);
                return new string(array);
            }

            return sides;
        }

        public void FlipHorizontal()
        {
            var rotated = new List<string>();
            for (var y = 0; y < TileSize; y++)
            {
                var sb = new StringBuilder();
                for (var x = 0; x < TileSize; x++)
                {
                    sb.Append(Image[y][TileSize - x - 1]);
                }
                rotated.Add(sb.ToString());
            }

            Image = rotated;
        }

        public void FlipVertical()
        {
            var rotated = new List<string>();
            for (var y = 0; y < TileSize; y++)
            {
                var sb = new StringBuilder();
                for (var x = 0; x < TileSize; x++)
                {
                    sb.Append(Image[TileSize - y - 1][x]);
                }
                rotated.Add(sb.ToString());

            }

            Image = rotated;
        }

        public void Rotate()
        {

            var rotated = new List<string>();

            for (var y = 0; y < TileSize; y++)
            {
                var sb = new StringBuilder();
                for (var x = 0; x < TileSize; x++)
                {
                    sb.Append(Image[TileSize - x - 1][y]);
                }
                rotated.Add(sb.ToString());
            }

            Image = rotated;
        }
    }
}
