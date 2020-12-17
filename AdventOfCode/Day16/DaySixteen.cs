using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode.Day16
{
    public static class DaySixteen
    {
        public static string ExecuteFirst()
        {
            var input = GetInput();
            var errorRate = input.NearbyTickets.Sum(nearbyTicket => (from ticketValue in nearbyTicket let ticketValid = input.Fields.Any(field => field.Ranges.Any(range => ticketValue >= range.Start && ticketValue <= range.End)) where !ticketValid select ticketValue).Sum());

            return errorRate.ToString();
        }

        public static string ExecuteSecond()
        {
            var input = GetInput();
            var validTickets = new List<List<int>>();

            foreach (var nearbyTicket in input.NearbyTickets)
            {
                var tickerValid = true;
                foreach (var _ in nearbyTicket.Where(ticketValue => !input.Fields.Any(field => field.Ranges.Any(range => ticketValue >= range.Start && ticketValue <= range.End))))
                {
                    tickerValid = false;
                }

                if (tickerValid)
                {
                    validTickets.Add(nearbyTicket);
                }
            }

            var values = new Dictionary<int, List<string>>();
            for (var i = 0; i < input.MyTicket.Count; i++)
            {
                foreach (var field in input.Fields)
                {
                    var fieldValid = true;
                    foreach (var validTicket in validTickets)
                    {
                        var inRange = false;
                        foreach (var _ in field.Ranges.Where(range => validTicket[i] >= range.Start && validTicket[i] <= range.End))
                        {
                            inRange = true;
                        }

                        if (!inRange)
                        {
                            fieldValid = false;
                            break;
                        }
                    }

                    if (fieldValid)
                    {
                        if (values.ContainsKey(i))
                        {
                            values[i].Add(field.FieldName);
                        }
                        else
                        {
                            values.Add(i, new List<string> { field.FieldName });
                        }
                    }
                }
            }

            while (values.Any(v => v.Value.Count > 1))
            {
                var singleValues = values.Where(v => v.Value.Count == 1).SelectMany(v => v.Value);
                foreach (var value in values.Where(v => v.Value.Count > 1))
                {
                    value.Value.RemoveAll(v => singleValues.Contains(v));
                }
            }

            var result = 1L;
            foreach (var value in values.Where(v => v.Value.Any(y => y.StartsWith("departure"))))
            {
                result *= input.MyTicket[value.Key];
            }

            return result.ToString();
        }

        private static TicketInput GetInput()
        {
            var ticketInput = new TicketInput();

            var fileStream = new FileStream("Day16/input.txt", FileMode.Open);
            using var reader = new StreamReader(fileStream);

            string input;
            while (!string.IsNullOrWhiteSpace(input = reader.ReadLine()))
            {
                var field = new Field();
                var components = input.Split(':');
                field.FieldName = components[0].Trim();

                var ranges = components[1].Split("or");
                foreach (var range in ranges)
                {
                    var rangeValues = range.Split('-');
                    field.Ranges.Add(new FieldRange { Start = int.Parse(rangeValues[0]), End = int.Parse(rangeValues[1]) });
                }

                ticketInput.Fields.Add(field);
            }

            reader.ReadLine();
            ticketInput.MyTicket.AddRange(reader.ReadLine().Split(',').Select(i => int.Parse(i)));
            reader.ReadLine();
            reader.ReadLine();


            while ((input = reader.ReadLine()) != null)
            {
                ticketInput.NearbyTickets.Add(input.Split(',').Select(int.Parse).ToList());

            }

            return ticketInput;
        }
    }
}
