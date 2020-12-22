using System.Collections.Generic;

namespace AdventOfCode.Day22
{
    public class Table
    {
        public Queue<int> YourCards { get; set; } = new Queue<int>();

        public Queue<int> CrabCards { get; set; } = new Queue<int>();
    }
}
