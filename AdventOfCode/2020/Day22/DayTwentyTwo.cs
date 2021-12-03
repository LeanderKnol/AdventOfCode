using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode._2020.Day22
{
    public static class DayTwentyTwo
    {
        public static string ExecuteFirst()
        {
            var input = GetInputArray();
            PlayGame(input);
            var winner = input.YourCards.Count > 0 ? input.YourCards : input.CrabCards;
            return winner.Reverse().Select((x, i) => x * (i + 1)).Sum().ToString();
        }


        public static string ExecuteSecond()
        {
            var input = GetInputArray();
            var youWin = PlayRecursiveGame(input.YourCards, input.CrabCards);
            var winner = youWin ? input.YourCards : input.CrabCards;
            return winner.Reverse().Select((x, i) => x * (i + 1)).Sum().ToString();
        }


        private static void PlayGame(Table table)
        {
            while (table.YourCards.Count > 0 && table.CrabCards.Count > 0)
            {
                var yourCard = table.YourCards.Dequeue();
                var crabCard = table.CrabCards.Dequeue();

                if (yourCard > crabCard)
                {
                    table.YourCards.Enqueue(yourCard);
                    table.YourCards.Enqueue(crabCard);
                }
                else
                {
                    table.CrabCards.Enqueue(crabCard);
                    table.CrabCards.Enqueue(yourCard);
                }
            }
        }


        private static bool PlayRecursiveGame(Queue<int> yourCards, Queue<int> crabCards)
        {
            var yourHistory = new List<string>();
            var crabHistory = new List<string>();

            var youWin = false;
            while (yourCards.Count > 0 && crabCards.Count > 0)
            {
                var yourRoundCards = string.Join(",", yourCards);
                var crabRoundCards = string.Join(",", crabCards);

                if (yourHistory.Contains(yourRoundCards) || crabHistory.Contains(crabRoundCards))
                {
                    return true;
                }

                yourHistory.Add(yourRoundCards);
                crabHistory.Add(crabRoundCards);

                youWin = false;
                var yourCard = yourCards.Dequeue();
                var crabCard = crabCards.Dequeue();

                if (yourCard <= yourCards.Count() && crabCard <= crabCards.Count())
                {
                    youWin = PlayRecursiveGame(new Queue<int>(yourCards.Take(yourCard)), new Queue<int>(crabCards.Take(crabCard)));
                }
                else
                {
                    if (yourCard > crabCard)
                    {
                        youWin = true;
                    }

                }

                if (youWin)
                {
                    yourCards.Enqueue(yourCard);
                    yourCards.Enqueue(crabCard);
                }
                else
                {
                    crabCards.Enqueue(crabCard);
                    crabCards.Enqueue(yourCard);
                }
            }
            return youWin;
        }

        private static Table GetInputArray()
        {
            var fileStream = new FileStream("Day22/input.txt", FileMode.Open);
            using var reader = new StreamReader(fileStream);

            var result = new Table();

            string input = reader.ReadLine();
            while ((!string.IsNullOrWhiteSpace(input = reader.ReadLine())))
            {
                result.YourCards.Enqueue(int.Parse(input));
            }

            input = reader.ReadLine();
            while ((input = reader.ReadLine()) != null)
            {
                result.CrabCards.Enqueue(int.Parse(input));

            }
            return result;
        }


    }
}
