using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Instrumentation
{
    public static class CoordinateHelpers
    {
        public static List<Coordinate2D> Neighbors(this Coordinate2D val, bool includeDiagonals = false)
        {
            var tmp = new List<Coordinate2D>()
            {
                new(val.x - 1, val.y),
                new(val.x + 1, val.y),
                new(val.x, val.y - 1),
                new(val.x, val.y + 1),
            };
            if (includeDiagonals)
            {
                tmp.AddRange(new List<Coordinate2D>()
                {
                    new(val.x - 1, val.y - 1),
                    new(val.x + 1, val.y - 1),
                    new(val.x - 1, val.y + 1),
                    new(val.x + 1, val.y + 1),
                });
            }
            return tmp;
        }
    }
}
