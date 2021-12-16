using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2021.Day16
{
    public class Packet
    {
        public int Version { get;  set; }
        public int Type { get;  set; }
        public long Value { get;  set; }
        public List<Packet> Children { get;  set; } = new List<Packet>();
    }
}
