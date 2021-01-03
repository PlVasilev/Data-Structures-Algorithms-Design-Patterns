namespace _01.Red_Black_Tree
{
    using System;
    using System.Collections.Generic;

    public class RedBlackTree<T> 
        : IBinarySearchTree<T> where T : IComparable
    {
        private const bool Red = true;
        private const bool Black = false;

        private Node _root;

        public RedBlackTree()
        {
        }
        private RedBlackTree(Node node)
        {
            _root = node;
        }

        public int Count => _root?.Count ?? 0;

        public void Insert(T element)
        {
           this._root = Insert(element,_root);
           this._root.Color = Black;
        }

        private Node Insert(T element, Node node)
        {
            if (node == null)
                return new Node(element){Count = 1};

            var comp = element.CompareTo(node.Value);
            if (comp > 0)
                node.Right = Insert(element, node.Right);
            else if(comp < 0)
                node.Left = Insert(element, node.Left);


            if (this.IsRed(node.Right) && !this.IsRed(node.Left))
                node = this.RotateLeft(node);
            if (this.IsRed(node.Left) && this.IsRed(node.Left.Left))
                node = this.RotateRight(node);
            if (this.IsRed(node.Left) && this.IsRed(node.Right))
                this.FlipColors(node);

            node.Count = 1 + GetCount(node.Left) + GetCount(node.Right);
            return node;

        }

        public T Select(int rank)
        {
            var node = Select(rank, _root);
            if (node == null)
                throw new InvalidOperationException();
            
            return node.Value;
        }

        private Node Select(int rank, Node node)
        {
            if (node == null) return null;

            var leftCount = GetCount(node.Left);
            if (leftCount == rank)
                return node;
            if (leftCount > rank)
                return Select(rank, node.Left);
            
            return Select(rank - (leftCount + 1), node.Right);
        }

        public int Rank(T element) => Rank(element, _root);
        

        private int Rank(T element, Node node)
        {
            if (node == null) return 0;
            
            var comp = element.CompareTo(node.Value);
            if (comp < 0)
                return this.Rank(element, node.Left);
            if (comp > 0)
                return 1 + GetCount(node.Left) + Rank(element, node.Right);

            return GetCount(node.Left);

        }

        public bool Contains(T element)
        {
            var node = FindElement(element);
            return node != null;
        }

        private Node FindElement(T element)
        {
            var current = this._root;
            while (current != null)
            {
                var comp = current.Value.CompareTo(element);
                if (comp > 0)
                    current = current.Left;
                else if (comp <0)
                    current = current.Right;
                else break;
            }
            return  current;
        }

        public IBinarySearchTree<T> Search(T element)
        {
            var result  = FindElement(element);
            return new RedBlackTree<T>(result);
        }

        public void DeleteMin()
        {
            if (this._root == null)
            {
                throw new InvalidOperationException();
            }

            this._root = DeleteMin(this._root);
        }

        private Node DeleteMin(Node node)
        {
            if (node.Left == null)
            {
                return node.Right;
            }
            node.Left = DeleteMin(node.Left);
            node.Count = 1 + GetCount(node.Left) + GetCount(node.Right);

            return node;
        }

        public void DeleteMax()
        {
            if (this._root == null)
            {
                throw new InvalidOperationException();
            }

            this._root = DeleteMax(this._root);
        }

        private Node DeleteMax(Node node)
        {
            if (node.Right == null)
            {
                return node.Left;
            }
            node.Right = DeleteMax(node.Right);
            node.Count = 1 + GetCount(node.Left) + GetCount(node.Right);

            return node;
        }

        public IEnumerable<T> Range(T startRange, T endRange)
        {
            var startRank = Rank(startRange);
            var endRank = Rank(endRange);
            var snapshot = RankSnapshot();
            for (int i = startRank; i <= endRank; i++)
            {
                yield return snapshot[i];
            }
        }

        private T[] RankSnapshot()
        {
            T[] snapshot = new T[this.Count];

            Queue<Node> Q = new Queue<Node>();
            Q.Enqueue(_root);
            while (Q.Count>0)
            {
                var node = Q.Dequeue();
                var nodeRank = this.Rank(node.Value);
                snapshot[nodeRank] = node.Value;
                if (node.Left != null)
                {
                    Q.Enqueue(node.Left);
                }

                if (node.Right != null)
                {
                    Q.Enqueue(node.Right);
                }
            }

            return snapshot;
        }

        public  void Delete(T element)
        {
            this._root = Delete(element, _root);
        }

        private Node Delete(T element, Node node)
        {
            if (node == null)
            {
                return null;
            }

            var comp = element.CompareTo(node.Value);
            if (comp >0 )
            {
                node.Right = Delete(element, node.Right);
            }
            else if (comp <0)
            {
                node.Left = Delete(element, node.Left);
            }
            else
            {
                if (node.Right == null)
                {
                    return node.Left;
                }

                if (node.Left == null)
                {
                    return node.Right;
                }

                Node temp = node;
                node = FindMin(temp.Right);
                node.Right = DeleteMin(temp.Right);
                node.Left = temp.Left;
            }

            node.Count = GetCount(node.Left) + GetCount(node.Right) + 1;
            return node;
        }

        private Node FindMin(Node node)
        {
            if (node.Left == null)
            {
                return node;
            }

            return FindMin(node.Left);
        }

        public T Ceiling(T element)
        {
            return Select(Rank(element) + 1);
        }

        public T Floor(T element)
        {
            return Select(Rank(element) - 1);
        }

        public void EachInOrder(Action<T> action)
        {
            EachInOrder(action,_root);
        }

        private void EachInOrder(Action<T> action, Node node)
        {
            if (node == null) return;

            EachInOrder(action,node.Left);
            action(node.Value);
            EachInOrder(action, node.Right);
        }

        private class Node
        {
            public Node(T value)
            {
                this.Value = value;
                Color = Red;
            }

            public T Value { get; }
            public Node Left { get; set; }
            public Node Right { get; set; }
            public int Count { get; set; }

            public bool Color { get; set; }
        }

        private bool IsRed(Node node)
        {
            if (node == null)
            {
                return false;
            }
            return node.Color == Red;

        }

        private Node RotateLeft(Node node)
        {
            Node temp = node.Right;
            node.Right = temp.Left;
            temp.Left = node;
            temp.Color = node.Color;
            node.Color = Red;
            node.Count = 1 + GetCount(node.Left) + GetCount(node.Right);

            return temp;
        }

        private int GetCount(Node node)
        {
            if (node == null)
            {
                return 0;
            }

            return node.Count;
        }

        private Node RotateRight(Node node)
        {
            Node temp = node.Left;
            node.Left = temp.Right;
            temp.Right = node;
            temp.Color = node.Color;
            node.Color = Red;
            node.Count = 1 + GetCount(node.Left) + GetCount(node.Right);

            return temp;
        }

        private void FlipColors(Node node)
        {
            node.Color = Red;
            node.Left.Color = Black;
            node.Right.Color = Black;
        }
    }
}