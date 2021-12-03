using System;
using System.Collections.Generic;
using System.IO;

namespace AdventOfCode._2020.Day12
{
    public static class DayTwelve
    {
        public static string ExecuteFirst()
        {
            var position = new Position
            {
                X = 0,
                Y = 0,
                CurrentDirection = "E"
            };

            var instructions = GetInput();

            foreach (var instruction in instructions)
            {
                position = MoveShipPosition(instruction, position);
            }

            var x = position.X < 0 ? position.X * -1 : position.X;
            var y = position.Y < 0 ? position.Y * -1 : position.Y;

            return (x + y).ToString();
        }

        private static Position MoveShipPosition(Direction instruction, Position position)
        {
            switch (instruction.Instruction)
            {
                case "N":
                    position.Y += instruction.Amount;
                    break;
                case "E":
                    position.X += instruction.Amount;
                    break;
                case "S":
                    position.Y -= instruction.Amount;
                    break;
                case "W":
                    position.X -= instruction.Amount;
                    break;
                case "F":
                    instruction.Instruction = position.CurrentDirection;
                    position = MoveShipPosition(instruction, position);
                    break;
                default:
                    for (var i = 1; i <= instruction.Amount / 90; i++)
                    {
                        position.CurrentDirection = GetNewDirection(instruction.Instruction, position.CurrentDirection);

                    }
                    break;

            }

            return position;
        }

        private static string GetNewDirection(string instruction, string position)
        {
            var directions = new[] { "N", "E", "S", "W" };

            var index = Array.IndexOf(directions, position);

            index = instruction == "L" ? index - 1 : index + 1;

            if (index > 3)
            {
                index -= 4;
            }
            else if (index < 0)
            {
                index += 4;
            }


            return directions[index];
        }

        public static string ExecuteSecond()
        {
            var position = new Position
            {
                X = 0,
                Y = 0,
                CurrentDirection = "E"
            };

            var wayPointPosition = new Position
            {
                X = 10,
                Y = 1,
            };


            var instructions = GetInput();


            foreach (var instruction in instructions)
            {
                if (instruction.Instruction == "F")
                {
                    position = MoveShipToWayPoint(instruction, position, wayPointPosition);
                }
                else
                {
                    wayPointPosition = MoveWayPointPosition(instruction, wayPointPosition);
                }
            }


            var x = position.X < 0 ? position.X * -1 : position.X;
            var y = position.Y < 0 ? position.Y * -1 : position.Y;

            return (x + y).ToString();
        }

        private static Position MoveShipToWayPoint(Direction instruction, Position position, Position wayPointPosition)
        {
            position.X += wayPointPosition.X * instruction.Amount;
            position.Y += wayPointPosition.Y * instruction.Amount;
            return position;
        }


        private static Position MoveWayPointPosition(Direction instruction, Position position)
        {
            switch (instruction.Instruction)
            {
                case "N":
                    position.Y += instruction.Amount;
                    break;
                case "E":
                    position.X += instruction.Amount;
                    break;
                case "S":
                    position.Y -= instruction.Amount;
                    break;
                case "W":
                    position.X -= instruction.Amount;
                    break;
                case "F":
                    break;
                default:
                    for (var i = 1; i <= instruction.Amount / 90; i++)
                    {
                        position = RotateWayPoint(instruction.Instruction, position);

                    }
                    break;
            }

            return position;
        }

        private static Position RotateWayPoint(string instruction, Position position)
        {
            var newX = position.Y;
            var newY = position.X;


            if (instruction == "R")
            {
                newY *= -1;

            }
            else
            {
                newX *= -1;
            }

            position.X = newX;
            position.Y = newY;
            return position;
        }


        private static List<Direction> GetInput()
        {
            var seating = new List<Direction>();

            var fileStream = new FileStream("Day12/input.txt", FileMode.Open);
            using var reader = new StreamReader(fileStream);
            string input;
            while ((input = reader.ReadLine()) != null)
            {

                seating.Add(new Direction
                {
                    Instruction = input.Substring(0, 1),
                    Amount = int.Parse(input.Substring(1))
                });
            }


            return seating;
        }
    }
}
