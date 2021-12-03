using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode._2020.Day8
{
    public static class DayEigth
    {
        public static string ExecuteFirst()
        {
            var instructions = GetInput();

            var acc = RunProgramm(instructions, out _);

            return acc.ToString();
        }

        private static int RunProgramm(List<string> instructions, out bool isInfiniteLoop)
        {
            var acc = 0;
            var accessedIndexes = new List<int>();
            var currentIndex = 0;
            var hasInstructions = true;
            isInfiniteLoop = true;

            while (hasInstructions)
            {
                if (accessedIndexes.Contains(currentIndex))
                {
                    hasInstructions = false;
                }
                else if (currentIndex == instructions.Count)
                {
                    hasInstructions = false;
                    isInfiniteLoop = false;
                }
                else
                {
                    accessedIndexes.Add(currentIndex);
                    var i = GetInstruction(instructions[currentIndex]);
                    switch (i.Inst)
                    {
                        case "acc":
                            acc += i.Amount;
                            currentIndex++;
                            break;
                        case "jmp":
                            currentIndex += i.Amount;
                            break;
                        case "nop":
                            currentIndex++;
                            break;
                    }
                }
            }

            return acc;
        }

        public static string ExecuteSecond()
        {
            var acc = 0;
            var lastRanIndex = 0;
            var instructions = GetInput();
            var isInfiniteLoop = true;
            while (isInfiniteLoop)
            {
                var testProgramm = instructions.ToList();
                var instruction = testProgramm.Skip(lastRanIndex + 1).FirstOrDefault(i => i.StartsWith("jmp") || i.StartsWith("nop"));
                if (instruction != null)
                {
                    var index = testProgramm.IndexOf(instruction, lastRanIndex + 1);
                    lastRanIndex = index;
                    testProgramm[index] = testProgramm[index].StartsWith("jmp") ? testProgramm[index].Replace("jmp", "nop") : testProgramm[index].Replace("nop", "jmp");
                    acc = RunProgramm(testProgramm, out isInfiniteLoop);
                }
            }

            return acc.ToString();
        }

        private static Instruction GetInstruction(string input)
        {
            var components = input.Split(' ');
            return new Instruction
            {
                Amount = int.Parse(components[1]),
                Inst = components[0]
            };
            ;
        }


        private static List<string> GetInput()
        {
            var instructions = new List<string>();

            var fileStream = new FileStream("Day8/input.txt", FileMode.Open);
            using var reader = new StreamReader(fileStream);
            string input;
            while ((input = reader.ReadLine()) != null)
            {
                instructions.Add(input);

            }
            return instructions;
        }
    }
}
