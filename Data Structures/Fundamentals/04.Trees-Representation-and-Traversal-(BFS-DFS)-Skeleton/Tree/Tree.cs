namespace Tree
{
    using System;
    using System.Collections.Generic;

    public class Tree<T> : IAbstractTree<T>
    {
        private readonly List<Tree<T>> _children;

        public Tree(T value)
        {
            this.Value = value;
            this.Parent = null;
            this._children = new List<Tree<T>>();
        }

        public Tree(T value, params Tree<T>[] children)
            : this(value)
        {
            foreach (var child in children)
            {
                child.Parent = this;
                this._children.Add(child);
            }
        }

        public T Value { get; private set; }
        public Tree<T> Parent { get; private set; }
        public IReadOnlyCollection<Tree<T>> Children => this._children.AsReadOnly();

        public bool IsRootDeleted { get; private set; }

        public ICollection<T> OrderBfs()
        {
            var result = new List<T>();
            if (this.IsRootDeleted)
            {
                return result;
            }

            var queue = new Queue<Tree<T>>();

            queue.Enqueue(this);
            while (queue.Count > 0)
            {
                var subTree = queue.Dequeue();
                result.Add(subTree.Value);
                foreach (var child in subTree.Children)
                {
                    queue.Enqueue(child);
                }
            }

            return result;
        }

        public ICollection<T> OrderDfs()
        {
             // with Stack
             // return this.StackOrderDfs();

             // with recursion
             var result = new List<T>();
             if (this.IsRootDeleted)
             {
                 return result;
             }
             this.Dfs(this, result); 
             return result;
        }

        private void Dfs(Tree<T> subTree, List<T> order)
        {
            foreach (var subTreeChild in subTree.Children)
            {
                this.Dfs(subTreeChild, order);
            }
            order.Add(subTree.Value);
        }

        private ICollection<T> StackOrderDfs()
        {
            var result = new Stack<T>();
            var toTraverse = new Stack<Tree<T>>();
            toTraverse.Push(this);

            while (toTraverse.Count > 0)
            {
                var subTree = toTraverse.Pop();
                foreach (var subTreeChild in subTree.Children)
                {
                    toTraverse.Push(subTreeChild);
                }
                result.Push(subTree.Value);
            }
            return new List<T>(result);
        }


        public void AddChild(T parentKey, Tree<T> child)
        {
            //DFS
            // var parentSubTree = this.FindDfs(parentKey,this);

            //BFS
            var parentSubTree = this.FindBfs(parentKey);
            this.CheckEmptyNode(parentSubTree);
            parentSubTree._children.Add(child);
        }

        private Tree<T> FindBfs(T value)
        {
            var queue = new Queue<Tree<T>>();
            queue.Enqueue(this);
            while (queue.Count > 0)
            {
                var subTree = queue.Dequeue();
                if (subTree.Value.Equals(value))
                {
                    return subTree;
                }

                foreach (var subTreeChild in subTree.Children)
                {
                    queue.Enqueue(subTreeChild);
                }
            }
            return null;
        }

        private Tree<T> FindDfs(T value, Tree<T> subtree)
        {
            foreach (var subtreeChild in subtree.Children)
            {
                Tree<T> current = this.FindDfs(value, subtreeChild);

                if (current != null && current.Value.Equals(value)) return current;
            }

            if (subtree.Value.Equals(value)) return subtree; // for root

            return null;
        }

        private void CheckEmptyNode(Tree<T> parentSubtree)
        {
            if (parentSubtree == null)
            {
                throw  new ArgumentNullException();
            }
        }

        public void RemoveNode(T nodeKey)
        {
            var currentNode = this.FindBfs(nodeKey);
            this.CheckEmptyNode(currentNode);
            foreach (var child in currentNode.Children)
            {
                child.Parent = null;
            }
            currentNode._children.Clear();
            var parentNode = currentNode.Parent;

            if (parentNode == null)
            {
                this.IsRootDeleted = true;
            }
            else
            {
                parentNode._children.Remove(currentNode);
                currentNode.Parent = null;
            }

            currentNode.Value = default(T);
        }

        public void Swap(T firstKey, T secondKey)
        {
            var firstNode = this.FindBfs(firstKey);
            var secondNode = this.FindBfs(secondKey);
            this.CheckEmptyNode(firstNode);
            this.CheckEmptyNode(secondNode);
            var firstParent = firstNode.Parent;
            var secondParent = secondNode.Parent;

            if (firstParent == null)
            {
                this.SwapRoot(secondNode);
                return;
            }

            if (secondParent == null)
            {
                this.SwapRoot(firstNode);
                return;
            }

            firstNode.Parent = secondParent;
            secondNode.Parent = firstParent;

            int indexOfFirst = firstParent._children.IndexOf(firstNode);
            int indexOfSecond = secondParent._children.IndexOf(secondNode);

            firstParent._children[indexOfFirst] = secondNode;
            secondParent._children[indexOfSecond] = firstNode;
        }

        private void SwapRoot(Tree<T> node)
        {
            this.Value = node.Value;
            this._children.Clear();
            foreach (var secondNodeChild in node.Children)
            {
                this._children.Add(secondNodeChild);
            }
        }
    }
}
