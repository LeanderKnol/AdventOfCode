using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode.Day5
{
    public static class DayFive
    {
        public static string ExecuteFirst()
        {
            var searIds = new List<int>();
            foreach (var boardingPass in GetInput())
            {
                searIds.Add(CalculateSeat(boardingPass));
            }
            return searIds.Max().ToString();
        }

        public static string ExecuteSecond()
        {
            var searIds = new List<int>();
            foreach (var boardingPass in GetInput())
            {
                searIds.Add(CalculateSeat(boardingPass));
            }

            var mySeat = 0;
            for (int i = searIds.Min(); i < searIds.Max(); i++)
            {
                if (!searIds.Contains(i) && searIds.Contains(i - 1) && searIds.Contains(i + 1))
                {
                    mySeat = i;
                }
            }

            return mySeat.ToString();
        }

        private static int CalculateSeat(string boardingPass)
        {
            var rows = new List<int>();
            for (int i = 0; i <= 127; i++)
            {
                rows.Add((i));
            }

            var cols = new List<int>();
            for (int i = 0; i <= 7; i++)
            {
                cols.Add((i));
            }

            foreach (var c in boardingPass)
            {
                switch (c)
                {
                    case 'F':
                    case 'B':
                        {
                            var dif = (decimal)(rows.Max() - rows.Min()) / 2;
                            if (c == 'F')
                                rows = rows.Where(r => r < (rows.Min() + dif)).ToList();
                            else
                                rows = rows.Where(r => r > (rows.Min() + dif)).ToList();
                            break;
                        }
                    case 'L':
                    case 'R':
                        {
                            var dif = (decimal)(cols.Max() - cols.Min()) / 2;
                            if (c == 'L')
                                cols = cols.Where(r => r < (cols.Min() + dif)).ToList();
                            else
                                cols = cols.Where(r => r > (cols.Min() + dif)).ToList();
                            break;
                        }
                }
            }

            return (rows.First() * 8) + cols.First();

        }

        private static List<string> GetInput()
        {
            var inputs = new List<string>();

            FileStream fileStream = new FileStream("Day5/input.txt", FileMode.Open);
            using (StreamReader reader = new StreamReader(fileStream))
            {
                string input;
                while ((input = reader.ReadLine()) != null)
                {
                    inputs.Add(input);
                }
            }

            return inputs;
        }
    }
}
