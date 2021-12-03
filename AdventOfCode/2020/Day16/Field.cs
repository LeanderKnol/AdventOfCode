using System.Collections.Generic;

namespace AdventOfCode._2020.Day16
{
    public class Field
    {
        public string FieldName { get; set; }

        public List<FieldRange> Ranges { get; set; } = new List<FieldRange>();
    }
}
