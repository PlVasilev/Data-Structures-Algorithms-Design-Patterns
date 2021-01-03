namespace Problem01.FasterQueue
{
    public class Node<T>
    {
        public T Item { get; set; }

        public Node<T> Next { get; set; }

        public Node(T value)
        {
            this.Item = value;
            this.Next = null;
        }
    }
}