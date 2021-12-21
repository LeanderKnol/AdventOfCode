using System;
using System.Linq;
using AdventOfCode.Instrumentation;
using AdventOfCode.Instrumentation.DataTypes;

namespace AdventOfCode._2021.Day18
{
    public class Day18
    {
        private readonly string[] _input;

        public Day18()
        {
            _input = Helpers.ReadInputArray<string>("2021/Day18/Input.txt");
            //_input = Helpers.ReadInputArray<string>("2021/Day18/ExampleInput.txt");
        }

        public void Part1()
        {
            var result = _input.Skip(1).Aggregate(Parse(_input[0]), (a, b) => Add(a, Parse(b)));
            Console.WriteLine(Magnitude(result.Root).ToString());
        }

        public void Part2()
        {
            long best = 0;
            for (int i = 0; i < _input.Length; i++)
            {
                for (int j = 0; j < _input.Length; j++)
                {
                    if (i != j)
                    {
                        var result = Add(Parse(_input[i]), Parse(_input[j]));
                        best = Math.Max(best, Magnitude(result.Root));
                    }
                }
            }
            Console.WriteLine(best);
        }

        private Tree<int> Add(Tree<int> a, Tree<int> b)
        {
            var reduced = new Tree<int>(new Node<int>(null, -1) { Left = a.Root, Right = b.Root });
            Reduce(reduced);
            return reduced;
        }

        private void Reduce(Tree<int> tree)
        {
            bool changed = true;
            while (changed)
            {
                changed = false;
                while (ReduceRecurse(tree.Root, Explode, tree))
                    changed = true;

                if (ReduceRecurse(tree.Root, Split, tree))
                    changed = true;
            }
        }

        private bool ReduceRecurse(Node<int> node, Func<Tree<int>, Node<int>, bool> reducer, Tree<int> tree)
        {
            if (reducer(tree, node))
                return true;

            if ((node.Left != null) && ReduceRecurse(node.Left, reducer, tree))
                return true;

            if ((node.Right != null) && ReduceRecurse(node.Right, reducer, tree))
                return true;

            return false;
        }

        private static Node<int> SiblingOf(Node<int> from, Func<Node<int>, Node<int>> move1, Func<Node<int>, Node<int>> move2)
        {
            var current = from;
            while (current.Parent != null)
            {
                if (move1(current.Parent) == current)
                {
                    var other = move2(current.Parent);
                    while (other.Data == -1)
                    {
                        other = move1(other) ?? move2(other);
                    }

                    return other;
                }

                current = current.Parent;
            }

            return null;
        }

        private static long Magnitude(Node<int> node)
        {
            if (node.Data >= 0)
            {
                return node.Data;
            }

            return 3 * Magnitude(node.Left) + 2 * Magnitude(node.Right);
        }

        private bool Split(Tree<int> tree, Node<int> node)
        {
            if (node.Data >= 10)
            {
                node.Left = new Node<int>(node, node.Data / 2);
                node.Right = new Node<int>(node, node.Data / 2 + node.Data % 2);
                node.Data = -1;
                return true;
            }

            return false;
        }

        private bool Explode(Tree<int> tree, Node<int> node)
        {
            if ((node.Data == -1) && (node.DistanceToParent(tree.Root) >= 4))
            {
                var left = SiblingOf(node, n => n.Right, n => n.Left);
                if (left != null)
                    left.Data += node.Left.Data;

                var right = SiblingOf(node, n => n.Left, n => n.Right);
                if (right != null)
                    right.Data += node.Right.Data;

                node.Left = null;
                node.Right = null;
                node.Data = 0;
                return true;
            }

            return false;
        }

        private Tree<int> Parse(string input)
        {
            int index = 0;
            return new(ParsePair(null, input, ref index));
        }

        Node<int> ParsePair(Node<int> parent, string input, ref int index)
        {
            if (input[index] == '[')
            {
                var node = new Node<int>(parent, -1);
                index++;
                node.Left = ParsePair(node, input, ref index);
                index++;
                node.Right = ParsePair(node, input, ref index);
                index++;
                return node;
            }

            return new Node<int>(parent, int.Parse(input[index++].ToString()));
        }

    }
}
