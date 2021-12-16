using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Instrumentation;

namespace AdventOfCode._2021.Day16
{
    public class Day16
    {
        private readonly Packet _packet;
        public Day16()
        {
            //_input = Helpers.ReadInputArray<string>("2021/Day16/LitteralExampleInput.txt").Single();
            //_input = Helpers.ReadInputArray<string>("2021/Day16/OperatorExampleInput.txt").Single();
            //_input = Helpers.ReadInputArray<string>("2021/Day16/Operator2ExampleInput.txt").Single();
            //_input = Helpers.ReadInputArray<string>("2021/Day16/ExampleInput.txt").Single();
            var input = Helpers.ReadInputArray<string>("2021/Day16/Input.txt").Single();


            string binaryString = string.Join(string.Empty, input.Select(c => Convert.ToString(Convert.ToInt32(c.ToString(), 16), 2).PadLeft(4, '0')));
            _packet = Run(binaryString, out _);
        }

        public void Part1()
        {
            Console.WriteLine(VersionSum(_packet));
        }

        public void Part2()
        {
            Console.WriteLine(RunOperations(_packet));
        }

        public Packet Run(string binaryString, out int index)
        {
            index = 0;
            var version = Convert.ToInt32(binaryString[index..(index + 3)], 2);
            index += 3;
            var type = Convert.ToInt32(binaryString[(index)..(index + 3)], 2);
            index += 3;

            if (type == 4)
            {
                var binaryValue = string.Empty;
                while (true)
                {
                    var group = binaryString[index..(index + 5)];
                    index += 5;

                    binaryValue += group[1..];
                    if (group[0] == '0')
                    {
                        break;
                    }
                }

                var value = Convert.ToInt64(binaryValue, 2);
                return new Packet { Version = version, Type = type, Value = value };
            }

            var lengthTypeId = binaryString[6];
            index++;

            List<Packet> children;
            if (lengthTypeId == '0')
            {
                var packetSize = Convert.ToInt32(binaryString[index..(index + 15)], 2);
                index += 15;
                var binarySubString = binaryString[index..(index + packetSize)];
                children = RunAll(binarySubString);
                index += packetSize;
            }
            else
            {
                var totalPackets = Convert.ToInt32(binaryString[index..(index + 11)], 2);
                index += 11;
                var binarySubString = binaryString[index..];
                children = RunAmount(binarySubString,totalPackets,out int endIndex);
                index += endIndex;
            }
            return new Packet { Version = version, Type = type, Children = children };
        }

        private List<Packet> RunAll(string binaryString)
        {
            var children = new List<Packet>();
            int index = 0;
            while (index < binaryString.Length-6)
            {
                Packet packet = Run(binaryString[index..], out int end);
                children.Add(packet);
                index += end;
            }

            return children;
        }

        private List<Packet> RunAmount(string binaryString, int amount, out int index)
        {
            var children = new List<Packet>();
            index = 0;
            for (int i = 0; i < amount; i++)
            {
                Packet packet = Run(binaryString[index..], out int end);
                children.Add(packet);
                index += end;
            }

            return children;
        }

        public int VersionSum(Packet packet)
        {
            return packet.Version + packet.Children.Sum(VersionSum);
        }

        public long RunOperations(Packet packet)
        {
            switch (packet.Type)
            {
                case 0: return packet.Children.Sum(RunOperations);
                case 1: return packet.Children.Aggregate((long)1, (a, b) => a * RunOperations(b));
                case 2: return packet.Children.Min(RunOperations);
                case 3: return packet.Children.Max(RunOperations);
                case 4: return packet.Value;
                case 5: return RunOperations(packet.Children[0]) > RunOperations(packet.Children[1]) ? 1 : 0;
                case 6: return RunOperations(packet.Children[0]) < RunOperations(packet.Children[1]) ? 1 : 0;
                case 7: return RunOperations(packet.Children[0]) == RunOperations(packet.Children[1]) ? 1 : 0;
            }

            throw new InvalidOperationException();
        }
    }
}
