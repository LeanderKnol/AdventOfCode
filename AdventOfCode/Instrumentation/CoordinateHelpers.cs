using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventOfCode.Instrumentation.DataTypes;

namespace AdventOfCode.Instrumentation
{
    public static class CoordinateHelpers
    {
        public static List<Coordinate2D> Neighbors(this Coordinate2D val, bool includeDiagonals = false)
        {
            var tmp = new List<Coordinate2D>()
            {
                new(val.X - 1, val.Y),
                new(val.X + 1, val.Y),
                new(val.X, val.Y - 1),
                new(val.X, val.Y + 1),
            };
            if (includeDiagonals)
            {
                tmp.AddRange(new List<Coordinate2D>()
                {
                    new(val.X - 1, val.Y - 1),
                    new(val.X + 1, val.Y - 1),
                    new(val.X - 1, val.Y + 1),
                    new(val.X + 1, val.Y + 1),
                });
            }
            return tmp;
        }
    }
}
