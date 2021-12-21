using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Instrumentation;

namespace AdventOfCode._2021.Day21
{
    public class Day21
    {
        private int _dice;
        private int _totalRoles;

        private List<Player> Init()
        {
            //var input = Helpers.ReadInputArray<string>("2021/Day21/ExampleInput.txt");
            var input = Helpers.ReadInputArray<string>("2021/Day21/Input.txt");
            List<Player> players = new();
            _dice = 1;

            foreach (var player in input)
            {
                var components = player.Split(':');
                var playerNr = components[0].Replace("Player", string.Empty).Replace("starting position", string.Empty).ToInt();
                players.Add(new(playerNr, components[1].ToInt(), 0));
            }

            return players;
        }

        public void Part1()
        {
           var players = Init();
            while (!players.Any(p => p.Score >= 1000))
            {
                foreach (var player in players)
                {
                    for (int i = 0; i < 3; i++)
                    {
                        DeterministicRole(player);
                    }
                    CalculateScore(player);
                    if (player.Score >= 1000)
                    {
                        break;
                    }
                }
            }

            var result = players.First(p => p.Score < 1000).Score * _totalRoles;
            Console.WriteLine(result);

        }

        public void Part2()
        {
            var players = Init();
            long player1Wins = 0;
            long player2Wins = 0;
            var win = 21;
            var quantumRoles = QuantumRoles();

            var universes = new Dictionary<(Player player1,Player player2,int currentPlayer),long> { { (players[0],players[1],0), 1 } };
            while (universes.Count > 0)
            {
                var newUniverses = new Dictionary<(Player player1, Player player2, int currentPlayer), long>();

                foreach (var universe in universes) 
                {
                    foreach (var quantumRole in quantumRoles)
                    {
                        var player1 = new Player(universe.Key.player1);
                        var player2 = new Player(universe.Key.player2);

                        var totalOccurrences = (universe.Value * quantumRole.Value);
                        if (universe.Key.currentPlayer == 0)
                        {
                            player1.Move(quantumRole.Key);
                            CalculateScore(player1);
                            if (player1.Score >= win)
                            {
                                player1Wins += totalOccurrences;
                                continue;
                            }
                        }
                        else
                        {
                            player2.Move(quantumRole.Key);
                            CalculateScore(player2);
                            if (player2.Score >= win)
                            {
                                player2Wins += totalOccurrences;
                                continue;
                            }
                        }

                        var newUniverse = (player1, player2, (universe.Key.currentPlayer + 1) % 2);

                        if (!newUniverses.ContainsKey(newUniverse))
                        {
                            newUniverses.Add(newUniverse,0);
                        };
                        newUniverses[newUniverse] += totalOccurrences;

                    }
                }
                universes = newUniverses;
            }

            Console.WriteLine(Math.Max(player1Wins, player2Wins));
        }

        public void DeterministicRole(Player player)
        {
            player.Move(_dice);

            _dice++;
            if (_dice > 100)
            {
                _dice %= 100;
            }
            _totalRoles++;
        }

        private void CalculateScore(Player player)
        {
            player.Score += player.Position;
        }

        public Dictionary<int, int> QuantumRoles()
        {
            var roles = new Dictionary<int, int>();

            for (int dice1 = 1; dice1 <= 3; dice1++)
            {
                for (int dice2 = 1; dice2 <= 3; dice2++)
                {
                    for (int dice3 = 1; dice3 <= 3; dice3++)
                    {
                        var currentRoll = dice1 + dice2 + dice3;
                        if (!roles.ContainsKey(currentRoll))
                        {
                            roles[currentRoll] = 0;
                        };
                        roles[currentRoll]++;
                    }
                }
            }

            return roles;
        }
    }
}
