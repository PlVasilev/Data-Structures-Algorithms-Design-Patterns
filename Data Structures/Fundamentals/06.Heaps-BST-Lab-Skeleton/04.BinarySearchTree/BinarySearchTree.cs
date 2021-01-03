using System.Collections.Generic;

namespace _04.BinarySearchTree
{
    using System;

    public class BinarySearchTree<T> : IAbstractBinarySearchTree<T>
        where T : IComparable<T>
    {
        public BinarySearchTree()
        {
        }


        private HashSet<int> set;

        public BinarySearchTree(Node<T> root)
        {
            this.Copy(root);
        }

        private void Copy(Node<T> root)
        {
            if (root != null)
            {
                this.Insert(root.Value);
                Copy(root.LeftChild);
                Copy(root.RightChild);
               
            }
        }

        public Node<T> Root { get; private set; }

        public Node<T> LeftChild { get; private set; }

        public Node<T> RightChild { get; private set; }

        public T Value => this.Root.Value;

        public bool Contains(T element)
        {
            var current = this.Root;
            while (current != null)
            {
                if (this.IsLess(element, current.Value))
                    current = current.LeftChild;
                else if (this.IsGreater(element, current.Value))
                    current = current.RightChild;
                else return true;
            }
            return false;
        }

        public void Insert(T element)
        {
            var toInsert = new Node<T>(element, null, null);
            if (Root == null)
                this.Root = toInsert;
            else
            {
                var current = this.Root;
                Node<T> prev = null;

                while (current != null)
                {
                    prev = current;
                    if (this.IsLess(element, current.Value))
                        current = current.LeftChild;
                    else if (this.IsGreater(element, current.Value))
                        current = current.RightChild;
                    else return;
                }

                if (IsLess(element, prev.Value))
                {
                    prev.LeftChild = toInsert;
                    if (this.LeftChild == null)
                        this.LeftChild = toInsert;

                }
                else
                {
                    prev.RightChild = toInsert;
                    if (this.RightChild == null)
                        this.RightChild = toInsert;
                }
            }
        }

        
        public IAbstractBinarySearchTree<T> Search(T element)
        {
            var current = this.Root;
            while (current != null && !this.AreEqual(element, current.Value))
            {
                if (this.IsLess(element, current.Value))
                {
                    current = current.LeftChild;
                }
                else if (this.IsGreater(element, current.Value))
                {
                    current = current.RightChild;
                }
            }
            return new BinarySearchTree<T>(current);
        }

        private bool IsLess(T element, T currentValue) => element.CompareTo(currentValue) < 0;
        private bool IsGreater(T element, T currentValue) => element.CompareTo(currentValue) > 0;
        private bool AreEqual(T element, T currentValue) => element.CompareTo(currentValue) == 0;
    }
}
