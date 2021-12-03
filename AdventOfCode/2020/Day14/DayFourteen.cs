using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode._2020.Day14
{
    public static class DayFourteen
    {
        public static string ExecuteFirst()
        {
            var instructions = GetInput();
            var memory = new Dictionary<int, long>();
            var currentMask = string.Empty;

            foreach (var instruction in instructions)
            {
                if (instruction.InstructionType == InstructionType.Mask)
                {
                    currentMask = instruction.Value;
                }
                else
                {
                    var newInstruction = instruction.Value.ToCharArray();
                    for (var i = 0; i < instruction.Value.Length; i++)
                    {
                        if (currentMask[i] != 'X')
                        {
                            newInstruction[i] = currentMask[i];
                        }
                    }
                    memory[instruction.Location] = Convert.ToInt64(new string(newInstruction), 2);
                }
            }

            return memory.Sum(m => m.Value).ToString();
        }

        public static string ExecuteSecond()
        {
            var instructions = GetInput();
            var memory = new Dictionary<long, long>();
            var currentMask = string.Empty;

            foreach (var instruction in instructions)
            {
                if (instruction.InstructionType == InstructionType.Mask)
                {
                    currentMask = instruction.Value;
                }
                else
                {
                    char[] reverseBinary = instruction.Location
                    .ToBinaryString(currentMask.Length)
                    .Select((item, i) => currentMask[i] == '1' || currentMask[i] == 'X' ? currentMask[i] : item)
                    .Reverse()
                    .ToArray();

                    List<int> floatingIndexes = reverseBinary.Select((item, index) => new { item, index }).Where(x => x.item == 'X').Select(x => x.index).ToList();

                    for (long i = 0; i < Math.Pow(2, floatingIndexes.Count); i++)
                    {
                        string currentCombo = i.ToBinaryString(floatingIndexes.Count);
                        for (int j = 0; j < currentCombo.Length; j++)
                        {
                            reverseBinary[floatingIndexes[j]] = currentCombo[j];
                        }

                        memory[new string(reverseBinary).ReverseString().BinaryToNumber()] = instruction.Value.BinaryToNumber();
                    }
                }
            }
            return memory.Sum(x => x.Value).ToString();
        }


        private static List<Instruction> GetInput()
        {
            var instructions = new List<Instruction>();

            var fileStream = new FileStream("Day14/input.txt", FileMode.Open);
            using var reader = new StreamReader(fileStream);

            string input;
            while ((input = reader.ReadLine()) != null)
            {
                var instruction = new Instruction();
                var components = input.Split("=");


                if (input.StartsWith("mask"))
                {
                    instruction.InstructionType = InstructionType.Mask;
                    instruction.Value = components[1].Trim();
                }
                else
                {
                    instruction.InstructionType = InstructionType.Memory;
                    var match = Regex.Match(components[0], @"\[(.+?)\]");
                    instruction.Location = int.Parse(match.Value.Substring(1, match.Value.Length - 2));
                    instruction.Value = Convert.ToString(int.Parse(components[1].Trim()), 2).PadLeft(36, '0');
                }

                instructions.Add(instruction);
            }

            return instructions;
        }
    }
}
