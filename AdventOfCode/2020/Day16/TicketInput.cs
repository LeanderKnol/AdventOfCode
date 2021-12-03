using System.Collections.Generic;

namespace AdventOfCode._2020.Day16
{
    public class TicketInput
    {
        public List<List<int>> NearbyTickets { get; set; } = new List<List<int>>();

        public List<Field> Fields { get; set; } = new List<Field>();

        public List<int> MyTicket { get; set; } = new List<int>();
    }
}
