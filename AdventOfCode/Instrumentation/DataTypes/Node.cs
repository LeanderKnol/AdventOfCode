using System.Collections.Generic;

namespace AdventOfCode.Instrumentation.DataTypes
{
    public class Node<T>
    {
        public Node<T> Parent { get; set; }
        public T Data { get; set; }
        public List<Node<T>> Children { get; } = new();

        public Node<T> Left
        {
            get
            {
                return this.Children.Count >= 1 ? this.Children[0] : null;
            }

            set
            {
                if (value != null)
                {
                    value.Parent = this;
                }
                if (this.Children.Count >= 1)
                {
                    this.Children[0] = value;
                }
                else
                {
                    this.Children.Add(value);
                }
            }
        }

        public Node<T> Right
        {
            get
            {
                return this.Children.Count >= 2 ? this.Children[1] : null;
            }

            set
            {
                if (value != null)
                {
                    value.Parent = this;
                }
                if (this.Children.Count >= 2)
                {
                    this.Children[1] = value;
                }
                else
                {
                    if (this.Children.Count == 0)
                    {
                        this.Children.Add(null);
                    }
                    this.Children.Add(value);
                }
            }
        }

        public long Weight;

        public long TotalWeight;

        public bool IsRoot
        {
            get
            {
                return Parent == null;
            }
        }

        public int DistanceToRoot
        {
            get
            {
                int dist = 0;
                var n = this;
                while (n.Parent != null)
                {
                    n = n.Parent;
                    dist++;
                }

                return dist;
            }
        }

        public Node(Node<T> parent, T data, long weight = 0)
        {
            Parent = parent;
            Data = data;
            Weight = weight;
            TotalWeight = 0;
        }

        public Node<T> AddChild(T data, long weight = 0)
        {
            Node<T> node = new Node<T>(this, data, weight);
            Children.Add(node);
            return node;
        }

        public void Traverse(Tree<T>.Visitor visitor)
        {
            visitor(Data);
            foreach (Node<T> child in Children)
            {
                child.Traverse(visitor);
            }
        }

        public Node<T> CommonParent(Node<T> child1, Node<T> child2)
        {
            Node<T> current = child1;
            while (current != this && !current.IsParentOf(child2))
            {
                current = current.Parent;
            }

            return current;
        }

        public bool IsParentOf(Node<T> child)
        {
            Node<T> current = child;
            while (!current.IsRoot && current != this)
            {
                current = current.Parent;
            }

            return current == this;
        }

        public int DistanceToParent(Node<T> parent)
        {
            Node<T> current = this;
            int dist = 0;
            while (current != parent)
            {
                dist++;
                current = current.Parent;
            }

            return dist;
        }
    }
}
