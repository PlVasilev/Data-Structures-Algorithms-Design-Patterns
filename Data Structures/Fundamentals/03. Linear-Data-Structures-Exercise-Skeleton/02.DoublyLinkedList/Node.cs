namespace Problem02.DoublyLinkedList
{
    public class Node<T>
    {
        public T Item { get; set; }

        public Node<T> Next { get; set; }
        public Node<T> Previous { get; set; }

        public Node()
        {
            this.Item = default;
            this.Next = null;
        }

        public Node(T value)
        {
            this.Item = value;
            this.Next = null;
        }
    }


}