namespace _01.BSTOperations
{
    using System;
    using System.Collections.Generic;

    public class BinarySearchTree<T> : IAbstractBinarySearchTree<T>
        where T : IComparable<T>
    {
        public BinarySearchTree() { }

        public BinarySearchTree(Node<T> root)
        {
            this.Copy(root);
        }

        private void Copy(Node<T> current)
        {
            // Pre Order
            if (current != null)
            {
                this.Insert(current.Value);
                this.Copy(current.LeftChild);
                this.Copy(current.RightChild);
            }
        }

        public Node<T> Root { get; private set; }

        public Node<T> LeftChild { get; private set; }

        public Node<T> RightChild { get; private set; }

        public T Value => this.Root.Value;

        public int Count => this.Root.Count;

        public bool Contains(T element)
        {
            Node<T> current = this.Root;
            while (current != null)
            {
                if (this.IsLees(element, current.Value))
                {
                    current = current.LeftChild;
                }
                else if (this.IsGreater(element, current.Value))
                {
                    current = current.RightChild;
                }
                else
                {
                    return true;
                }
            }
            return false;
        }

        public void Insert(T element)
        {
            Node<T> toInsert = new Node<T>(element,null,null);
            if (this.Root == null)
            {
                this.Root = toInsert;
            }
            else
            {
                this.InsertElementDfs(Root,null, toInsert);
            }
        }
        private void InsertElementDfs(Node<T> current, Node<T> previus, Node<T> toInsert)
        {
            if (current == null && this.IsLees(toInsert.Value, previus.Value))
            {
                previus.LeftChild = toInsert;
                if (this.LeftChild == null)
                {
                    this.LeftChild = toInsert;
                }
                return;
            }
            if (current == null && this.IsGreater(toInsert.Value, previus.Value))
            {
                if (this.RightChild == null)
                {
                    this.RightChild = toInsert;
                }
                previus.RightChild = toInsert;
                return;
            }


            if (this.IsLees(toInsert.Value, current.Value))
            {
                this.InsertElementDfs(current.LeftChild, current, toInsert);
                current.Count++;
            }
            else if (this.IsGreater(toInsert.Value, current.Value))
            {
                this.InsertElementDfs(current.RightChild, current, toInsert);
                current.Count++;
            }
        }
        private bool IsLees(T firstEl, T secondEl) => firstEl.CompareTo(secondEl) < 0;
        private bool IsGreater(T firstEl, T secondEl) => firstEl.CompareTo(secondEl) > 0;
        private bool AreEqual(T firstEl, T secondEl) => firstEl.CompareTo(secondEl) == 0;

        public IAbstractBinarySearchTree<T> Search(T element)
        {
            Node<T> current = this.Root;

            while (current != null)
            {
                if (this.IsLees(element, current.Value))
                {
                    current = current.LeftChild;
                }
                else if (this.IsGreater(element, current.Value))
                {
                    current = current.RightChild;
                }
                else
                {
                    break;
                }
            }
            return new BinarySearchTree<T>(current);
        }

        public void EachInOrder(Action<T> action)
        {
            this.EachInOrderDfs(this.Root, action);
        }
        private void EachInOrderDfs(Node<T> current, Action<T> action)
        {
            if (current != null)
            {
                // In Order
               this.EachInOrderDfs(current.LeftChild, action);
               action.Invoke(current.Value);
               this.EachInOrderDfs(current.RightChild, action);
               //   // Pre Order
               //   action.Invoke(current.Value);
               //   this.EachInOrderDfs(current.LeftChild, action);
               //   this.EachInOrderDfs(current.RightChild, action);
               //   // Post Order
               //   this.EachInOrderDfs(current.LeftChild, action);
               //   this.EachInOrderDfs(current.RightChild, action);
               //   action.Invoke(current.Value);
            }
        }

        public List<T> Range(T lower, T upper)
        {
            var result = new List<T>();
            var nodes = new Queue<Node<T>>();
            nodes.Enqueue(Root);

            while (nodes.Count > 0)
            {
                var current = nodes.Dequeue();
                if (this.IsLees(lower, current.Value) && this.IsGreater(upper, current.Value))
                {
                    result.Add(current.Value);
                }else if (this.AreEqual(lower,current.Value) || this.AreEqual(upper, current.Value))
                {
                    result.Add(current.Value);
                }

                if (current.LeftChild != null)
                {
                    nodes.Enqueue(current.LeftChild);
                }

                if (current.RightChild != null)
                {
                    nodes.Enqueue(current.RightChild);
                }
            }
            return result;
        }

        public void DeleteMin()
        {
            if (Root == null)
            {
                throw new InvalidOperationException();
            }
            Node<T> current = Root;
            Node<T> previous = null;

            if (this.Root.LeftChild == null)
            {
                this.Root = this.RightChild;
            }
            else
            {
                while (current.LeftChild != null)
                {
                    current.Count--;
                    previous = current;
                    current = current.LeftChild;
                }
                previous.LeftChild = current.RightChild;
            }
        }

        public void DeleteMax()
        {
            if (Root == null)
            {
                throw new InvalidOperationException();
            }

            Node<T> current = Root;
            Node<T> previous = null;
            if (this.Root.RightChild == null)
            {
                this.Root = this.LeftChild;
            }
            else
            {
                while (current.RightChild != null)
                {
                    current.Count--;
                    previous = current;
                    current = current.RightChild;
                }

                previous.RightChild = current.LeftChild;
            }
        }

        public int GetRank(T element)
        {
            return this.GetRankDfs(Root, element);
        }
        private int GetRankDfs(Node<T> current, T element)
        {
            if (current == null)
            {
                return 0;
            }

            if (this.IsLees(element, current.Value))
            {
                return this.GetRankDfs(current.LeftChild, element);
            }
            else if (this.AreEqual(element, current.Value))
            {
                return this.GetNodeCount(current);
            }

            //  return current.LeftChild?.Count + 1 + this.GetRankDfs(current.RightChild, element) ?? 0;
            return this.GetNodeCount(current.LeftChild) + 1 + this.GetRankDfs(current.RightChild, element);
        }
        private int GetNodeCount(Node<T> current) => current?.Count ?? 0;
        
    }
}
