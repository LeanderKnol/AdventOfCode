namespace AdventOfCode._2020.Day14
{
    public class Instruction
    {
        public InstructionType InstructionType { get; set; }
        public int Location { get; set; }
        public string Value { get; set; }
    }

    public enum InstructionType
    {
        Mask,
        Memory
    }
}
