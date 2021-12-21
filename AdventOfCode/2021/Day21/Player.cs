using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2021.Day21
{
    public class Player
    {
        public int Id { get; set; }
        public int Position{ get; set; }
        public int Score { get; set; }

        public Player(int id, int position, int score)
        {
            Id = id;
            Position = position;
            Score = score;
        }

        public Player(Player player)
        {
            Id = player.Id;
            Position = player.Position;
            Score = player.Score;
        }

        public void Move(int dice)
        {
            Position += dice;
            if (Position > 10)
            {
                Position %= 10;
            }
            if (Position == 0)
            {
                Position = 10;
            }

        }

        public override string ToString()
        {
            return $"I={Id} - P={Position} - S={Score}";
        }
    }
}
