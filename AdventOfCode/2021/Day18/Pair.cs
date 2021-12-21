using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2021.Day18
{
    internal class Pair
    {
        public Pair(int value, int level)
        {
            Value = value;
            Level = level;
        }

        public int Value { get; set; }
        public int Level { get; init; }
    }
}
