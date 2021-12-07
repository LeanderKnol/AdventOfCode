namespace AdventOfCode._2021.Day4
{
    public class BingoCard
    {
        public BingoNumber[][] BingoNumbers { get; set; } = new BingoNumber[5][];
        public bool HasBingo { get; set; }
    }
    public class BingoNumber    
    {
        public int Number { get; set; }
        public bool Checked { get; set; }

        public BingoNumber(int number)
        {
            Number = number;
        }
    }
}
