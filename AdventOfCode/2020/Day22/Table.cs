using System.Collections.Generic;

namespace AdventOfCode._2020.Day22
{
    public class Table
    {
        public Queue<int> YourCards { get; set; } = new Queue<int>();

        public Queue<int> CrabCards { get; set; } = new Queue<int>();
    }
}
