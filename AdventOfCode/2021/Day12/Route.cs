using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2021.Day12
{
    internal class Route
    {
        public string CaveName { get; set; }
        public List<string> Path { get; set; } = new();
        public bool VisitedSmallCave { get; set; }

        public override string ToString()
        {
            return Path.Aggregate(string.Empty, (current, p) => $"{current},{p}");
        }
    }
}
