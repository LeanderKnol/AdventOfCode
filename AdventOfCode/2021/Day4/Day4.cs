using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode._2021.Day4
{
    public class Day4
    {
        private BingoCard _lastCard;
        private bool _firstDone;
        private bool _lastDone;
        private readonly List<BingoCard> _bingoCards = new();
        private int[] _checkNumbers;
        public Day4()
        {
            //ReadInput("2021/Day4/ExampleInput.txt");
            ReadInput("2021/Day4/Input.txt");

        }

        public void Part1And2()
        {
            foreach (var checkNumber in _checkNumbers)
            {
                foreach (var bingoCard in _bingoCards)
                {
                    CheckNumber(bingoCard, checkNumber);
                }

                foreach (var bingoCard in _bingoCards)
                {
                    if (HasBingo(bingoCard))
                    {
                        bingoCard.HasBingo = true;
                    }
                }


                if (_bingoCards.Count(bc => bc.HasBingo) == 1 && !_firstDone)
                {
                    _firstDone = true;
                    var bingoCard = _bingoCards.First(bc => bc.HasBingo);

                    var total = bingoCard.BingoNumbers.SelectMany(r => r.Where(n => !n.Checked).Select(n => n.Number)).Sum();
                    Console.WriteLine(total * checkNumber);
                }

                if (_bingoCards.Count(bc => !bc.HasBingo) == 1 && !_lastDone)
                {
                    _lastCard = _bingoCards.First(bc => !bc.HasBingo);

                }

                if (_bingoCards.Count(bc => !bc.HasBingo) == 0 && !_lastDone)
                {
                    _lastDone = true;

                    var total = _lastCard.BingoNumbers.SelectMany(r => r.Where(n => !n.Checked).Select(n => n.Number)).Sum();
                    Console.WriteLine(total * checkNumber);
                }
            }
        }
       
        private void ReadInput(string path)
        {
            FileStream fileStream = new FileStream(path, FileMode.Open);
            using StreamReader reader = new StreamReader(fileStream);

            var firstLine = reader.ReadLine();
            _checkNumbers = firstLine.Split(',').Select(int.Parse).ToArray();
            reader.ReadLine(); //Read empty line

            string input;
            var bingoCard = new BingoCard();
            int i = 0;
            while ((input = reader.ReadLine()) != null)
            {
                if (input != string.Empty)
                {
                    bingoCard.BingoNumbers[i] = input.Split(' ').Where(n => !string.IsNullOrEmpty(n)).Select(n => new BingoNumber(int.Parse(n))).ToArray();
                    i++;
                }
                else
                {
                    _bingoCards.Add(bingoCard);
                    i = 0;
                    bingoCard = new BingoCard();
                }
            }
            _bingoCards.Add(bingoCard);
        }

        private bool HasBingo(BingoCard bingoCard)
        {
            if (bingoCard.BingoNumbers.Any(row => row.All(n => n.Checked)))
            {
                return true;
            }

            for (int i = 0; i < 5; i++)
            {
                if (bingoCard.BingoNumbers.Select(n => n[i]).All(n => n.Checked))
                {
                    return true;
                }
            }

            return false;
        }

        private void CheckNumber(BingoCard bingoCard, int checkNumber)
        {
            foreach (var row in bingoCard.BingoNumbers)
            {
                foreach (var number in row)
                {
                    if (number.Number == checkNumber)
                    {
                        number.Checked = true;
                    }
                }
            }

        }
    }
}
