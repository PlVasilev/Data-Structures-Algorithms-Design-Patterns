namespace Problem03.Queue
{
    public class Node<T>
    {
        public T Element { get; set; }
        public Node<T> Next { get; set; }
        public Node<T> Previous { get; set; }

        public Node(T value)
        {
            this.Element = value;
            this.Next = null;
        }
    }
}