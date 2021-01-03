using System.Collections.Generic;

namespace _01.Hierarchy
{
    public class Node<T>
    {
        public T Value { get; }
        public Node<T> Parent { get;set; }
        public List<Node<T>> Children { get; }

        public Node(T value)
        {
            Value = value;
            Children = new List<Node<T>>();
        }

        public override int GetHashCode() => Value.GetHashCode();
    }
}
