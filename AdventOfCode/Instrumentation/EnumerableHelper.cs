using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Instrumentation
{
    internal static class EnumerableHelper
    {
        public static int[] ToIntArray(this string input, string delimiter = "")
        {
            if (delimiter == "")
            {
                List<int> result = new();
                foreach (var c in input)
                {
                    if (int.TryParse(c.ToString(), out int n))
                    {
                        result.Add(n);
                    }
                };
                return result.ToArray();
            }

            return input
                .Split(delimiter)
                .Where(n => int.TryParse(n, out int v))
                .Select(n => Convert.ToInt32(n))
                .ToArray();
        }

        public static void SafeIncrement<TKey>(this Dictionary<TKey,long> dictionary, TKey key, long value)
        {
            if (dictionary.ContainsKey(key))
            {
                dictionary[key] += value;
            }
            else
            {
                dictionary.Add(key, value);
            }
        }

        public static void SafeIncrement<TKey>(this Dictionary<TKey, int> dictionary, TKey key, int value)
        {
            if (dictionary.ContainsKey(key))
            {
                dictionary[key] += value;
            }
            else
            {
                dictionary.Add(key, value);
            }
        }

        public static void SafeUpdate<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, TKey key, TValue value)
        {
            if (dictionary.ContainsKey(key))
            {
                dictionary[key] = value;
            }
            else
            {
                dictionary.Add(key, value);
            }
        }
    }
}
