using System;
using System.IO;
using System.Linq;

namespace AdventOfCode.Day13
{
    public static class DayThirteen
    {
        public static string ExecuteFirst()
        {
            var notes = GetInput();
            var closestDiff = 0;
            var butToTake = 0;

            foreach (var busId in notes.BusIds)
            {
                var time = 0;
                while (time < notes.DepartureTime)
                {
                    time += busId;
                }

                if (closestDiff == 0 || time - notes.DepartureTime < closestDiff)
                {
                    closestDiff = time - notes.DepartureTime;
                    butToTake = busId;
                }
            }

            return (butToTake * closestDiff).ToString();
        }


        public static string ExecuteSecondB()
        {
            var notes = GetInput();


            long answer = ChineseRemainderTheorem(
                notes.Schedule
                    .Where(x => x > 0)
                    .Select(x => (long)x)
                    .ToArray(),
                notes.Schedule
                    .Select((x, i) => new { i, x })
                    .Where(x => x.x > 0)
                    .Select(x => (long)(x.x - x.i) % x.x) //(Bus ID - Position) % Bus ID
                    .ToArray()
            );
            return answer.ToString();
        }

        private static long ChineseRemainderTheorem(long[] n, long[] a)
        {
            static long ModularMultiplicativeInverse(long a, long mod)
            {
                var b = a % mod;

                for (var x = 1; x < mod; x++)
                {
                    if ((b * x) % mod == 1)
                    {
                        return x;
                    }
                }

                return 1;
            }

            var prod = n.Aggregate(1, (long i, long j) => i * j);
            long sm = 0;

            for (var i = 0; i < n.Length; i++)
            {
                var p = prod / n[i];

                sm += a[i] * ModularMultiplicativeInverse(p, n[i]) * p;
            }

            return sm % prod;
        }

        public static string ExecuteSecond()
        {
            var notes = GetInput();

            //var timestamp = 100043637894342;
            var timestamp = 294354277694069;
            while (true)
            {
                var i = 1;
                var failed = false;
                foreach (var s in notes.Schedule.Skip(1))
                {
                    if (s != 0 && (timestamp + i) % s != 0)
                    {
                        failed = true;
                        break;
                    }
                    i++;
                }

                if (!failed)
                {
                    return timestamp.ToString();
                }

                if (timestamp < 0)
                {
                    throw new InvalidOperationException();
                }
                timestamp += notes.BusIds[0];
            }
        }


        private static Notes GetInput()
        {
            var notes = new Notes();

            var fileStream = new FileStream("Day13/input.txt", FileMode.Open);
            using var reader = new StreamReader(fileStream);

            string input = reader.ReadLine();
            notes.DepartureTime = int.Parse(input ?? string.Empty);

            input = reader.ReadLine();

            notes.BusIds = input.Split(',').Where(id => id != "x").Select(int.Parse).ToList();
            notes.Schedule = input.Split(',').Select(x => int.TryParse(x, out var e) ? e : 0).ToList();
            return notes;
        }
    }
}
