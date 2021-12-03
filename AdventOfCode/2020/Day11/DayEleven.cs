using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode._2020.Day11
{
    public static class DayEleven
    {
        public static string ExecuteFirst()
        {
            var initialSeating = GetInput();

            var previousSeating = DeepCopyValue(initialSeating);
            var newSeating = SeatingRoundSurrounding(initialSeating);

            while (!DeepSequenceEqual(previousSeating, newSeating))
            {
                previousSeating = DeepCopyValue(newSeating);
                newSeating = SeatingRoundSurrounding(newSeating);
            }

            return newSeating.Sum(r => r.Count(s => s == '#')).ToString();
        }

        public static string ExecuteSecond()
        {
            var initialSeating = GetInput();

            var previousSeating = DeepCopyValue(initialSeating);
            var newSeating = SeatingRoundSighting(initialSeating);

            while (!DeepSequenceEqual(previousSeating, newSeating))
            {
                previousSeating = DeepCopyValue(newSeating);
                newSeating = SeatingRoundSighting(newSeating);
            }

            return newSeating.Sum(r => r.Count(s => s == '#')).ToString();
        }

        private static char[][] SeatingRoundSurrounding(char[][] seating)
        {
            var newSeating = DeepCopyValue(seating);
            for (var i = 0; i < seating.Length; i++)
            {
                for (var j = 0; j < seating[i].Length; j++)
                {
                    newSeating[i][j] = GetSeatValueFromSurroundings(seating, i, j);
                }
            }

            return newSeating;
        }

        private static char GetSeatValueFromSurroundings(char[][] seating, int row, int col)
        {
            var rowMin = row - 1 < 0 ? 0 : row - 1;
            var rowMax = row + 2 > seating.Length ? seating.Length : row + 2;

            var colMin = col - 1 < 0 ? 0 : col - 1;
            var colMax = col + 2 > seating[row].Length ? seating[row].Length : col + 2;


            var seatBox = seating[rowMin..rowMax].SelectMany(r => r[colMin..colMax]).ToList();

            if (seating[row][col] == 'L' && !seatBox.Contains('#'))
            {
                return '#';
            }
            if (seating[row][col] == '#' && seatBox.Count(c => c == '#') >= 5) //5 includes it's own occupation
            {
                return 'L';
            }

            return seating[row][col];
        }


        private static char[][] SeatingRoundSighting(char[][] seating)
        {
            var newSeating = DeepCopyValue(seating);
            for (var i = 0; i < seating.Length; i++)
            {
                for (var j = 0; j < seating[i].Length; j++)
                {
                    newSeating[i][j] = GetSeatValueFromSighting(seating, i, j);
                }
            }

            return newSeating;
        }

        private static char GetSeatValueFromSighting(char[][] seating, int row, int col)
        {

            var dimensions = new[] { (0, 1), (0, -1), (1, 0), (-1, 0), (1, 1), (1, -1), (-1, 1), (-1, -1) };
            var neighbours = new char[8];


            for (var i = 0; i < dimensions.Length; i++)
            {
                var iRow = row + dimensions[i].Item1;
                var iCol = col + dimensions[i].Item2;

                while (iRow >= 0 && iRow < seating.Length && iCol >= 0 && iCol < seating[row].Length && seating[iRow][iCol] == '.')
                {
                    iRow += dimensions[i].Item1;
                    iCol += dimensions[i].Item2;
                }
                if ((iRow != row || iCol != col) && iRow >= 0 && iRow < seating.Length && iCol >= 0 && iCol < seating[row].Length)
                {
                    neighbours[i] = seating[iRow][iCol];
                }
            }


            if (seating[row][col] == 'L' && !neighbours.Contains('#'))
            {
                return '#';
            }
            if (seating[row][col] == '#' && neighbours.Count(c => c == '#') >= 5)
            {
                return 'L';
            }

            return seating[row][col];
        }

        private static char[][] DeepCopyValue(char[][] seating)
        {
            var copiedSeating = new char[seating.Length][];
            for (var i = 0; i < seating.Length; i++)
            {
                var newRow = new char[seating[i].Length];
                seating[i].CopyTo(newRow, 0);
                copiedSeating[i] = newRow;
            }

            return copiedSeating;
        }

        private static bool DeepSequenceEqual(char[][] previousSeating, char[][] newSeating)
        {
            for (int i = 0; i < previousSeating.Length; i++)
            {
                if (!previousSeating[i].SequenceEqual(newSeating[i]))
                {
                    return false;
                }
            }
            return true;

        }

        private static char[][] GetInput()
        {
            var seating = new List<char[]>();

            var fileStream = new FileStream("Day11/input.txt", FileMode.Open);
            using var reader = new StreamReader(fileStream);
            string input;
            while ((input = reader.ReadLine()) != null)
            {

                seating.Add(input.ToCharArray());
            }


            return seating.ToArray();
        }
    }
}
