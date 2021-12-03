using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode._2020.Day23
{
    public static class DayTwentyThree
    {
        public static string ExecuteFirst()
        {
            var input = new LinkedList<int>("871369452".ToCharArray().Select(c => int.Parse(c.ToString())));

            return PlayGame(input, 100, 1);
        }


        public static string ExecuteSecond()
        {
            var input = new LinkedList<int>("871369452".ToCharArray().Select(c => int.Parse(c.ToString())));
            var next = input.Max() + 1;
            for (var i = input.Count; i < 1000000; i++)
            {
                input.AddLast(next);
                next++;
            }

            return PlayGame(input, 10000000, 2);
        }

        public static string PlayGame(LinkedList<int> numbers, int rounds, int roundNr)
        {
            var min = numbers.Min();
            var max = numbers.Max();

            var lookup = BuildDictionaryOfListNodes(numbers);


            var current = numbers.First;
            for (int i = 0; i < rounds; i++)
            {
                var removed = current.RemoveNAfter(3);
                var destination = FindDestinationNode(current.Value, removed, min, max, lookup);
                destination.AddNAfter(removed);
                current = current.NextOrFirst();
            }

            if (roundNr == 1)
            {
                var after = numbers.Find(1);
                var nxt = after.NextOrFirst();
                var sb = new StringBuilder();
                while (nxt != after)
                {
                    sb.Append(nxt.Value);
                    nxt = nxt.NextOrFirst();
                }

                return sb.ToString();
            }
            else
            {
                var one = numbers.Find(1);
                var nxt = one.NextOrFirst();
                var nxt2 = nxt.NextOrFirst();

                return ((long)nxt.Value * nxt2.Value).ToString();
            }
        }

        private static Dictionary<int, LinkedListNode<int>> BuildDictionaryOfListNodes(LinkedList<int> numbers)
        {
            var lookup = new Dictionary<int, LinkedListNode<int>>();
            var current = numbers.First;
            do
            {
                lookup[current.Value] = current;
                current = current.Next;
            } while (current != null);

            return lookup;
        }

        private static LinkedListNode<int> FindDestinationNode(int current,
            List<LinkedListNode<int>> removed, int min, int max,
            Dictionary<int, LinkedListNode<int>> lookup)
        {
            var destinationNumber = current - 1;

            LinkedListNode<int> destination;
            var removedValues = removed.Select(x => x.Value).ToList();
            for (; ; )
            {
                if (removedValues.Contains(destinationNumber))
                {
                    destinationNumber--;
                }
                else if (destinationNumber < min)
                {
                    destinationNumber = max;
                }
                else
                {
                    destination = lookup[destinationNumber];
                    break;
                }
            }

            return destination;
        }

        public static List<LinkedListNode<T>> RemoveNAfter<T>(this LinkedListNode<T> item, int count)
        {
            var list = item.List;
            var items = new List<LinkedListNode<T>>();
            for (var i = 0; i < count; i++)
            {
                item = NextOrFirst(item);
                items.Add(item);
            }

            foreach (var i in items)
            {
                list.Remove(i);
            }

            return items;
        }

        public static void AddNAfter<T>(this LinkedListNode<T> item, IEnumerable<LinkedListNode<T>> items)
        {
            var list = item.List;
            foreach (var next in items)
            {
                list.AddAfter(item, next);
                item = next;
            }
        }

        public static LinkedListNode<T> NextOrFirst<T>(this LinkedListNode<T> item)
        {
            return item.Next ?? item.List.First;
        }
    }
}
