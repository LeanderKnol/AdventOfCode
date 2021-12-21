namespace AdventOfCode.Instrumentation.DataTypes
{
    public class Tree<T>
    {
        public delegate void Visitor(T nodeData);

        public Node<T> Root { get; set; }

        public Tree(T data)
        {
            Root = new Node<T>(null, data, 0);
        }

        public Tree(Node<T> root)
        {
            Root = root;
        }

        public long GetWeight(Node<T> node)
        {
            long weight = node.Weight;
            foreach (var child in node.Children)
            {
                weight += GetWeight(child);
            }

            node.TotalWeight = weight;
            return weight;
        }
    }
}
