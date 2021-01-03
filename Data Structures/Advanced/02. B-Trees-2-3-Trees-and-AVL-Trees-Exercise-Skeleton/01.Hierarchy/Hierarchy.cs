using System.Linq;

namespace _01.Hierarchy
{
    using System;
    using System.Collections.Generic;
    using System.Collections;

    public class Hierarchy<T> : IHierarchy<T>
    {
        private readonly Node<T> _root;
        private readonly Dictionary<T, Node<T>> _allElements;

        public Hierarchy(T root)
        {
            _allElements = new Dictionary<T, Node<T>>();
            _root = CreateNode(root);
            
        }

        public int Count => _allElements.Count;

        public void Add(T element, T child)
        {
            ContainsOrThrowException(element);
            if (_allElements.ContainsKey(child)) 
                throw new ArgumentException();

            var node = CreateNode(child);
            node.Parent = _allElements[element];
            _allElements[element].Children.Add(node);
        }

        private Node<T> CreateNode(T element)
        {
            var node = new Node<T>(element);
            _allElements[element] = node;
            return node;
        }

        public void Remove(T element)
        {
            if (_root.Value.Equals(element))
                throw new InvalidOperationException();
            ContainsOrThrowException(element);

            RemoveElement(element);
        }

        private void RemoveElement(T element)
        {
            var node = _allElements[element];
            node.Parent?.Children.Remove(node);
            if (node.Parent != null && node.Children.Count > 0)
            {
                foreach (var child in node.Children)
                {
                    child.Parent = node.Parent;
                    node.Parent.Children.Add(child);
                }
            }
            _allElements.Remove(element);
        }

        public IEnumerable<T> GetChildren(T element)
        {
            ContainsOrThrowException(element);
            return _allElements[element].Children.Select(n => n.Value);
        }

        public T GetParent(T element)
        {
            ContainsOrThrowException(element);
            var node = _allElements[element];
            return node.Parent != null ? node.Parent.Value : default(T);
        }

        public bool Contains(T element)
        {
            return _allElements.ContainsKey(element);
        }

        public IEnumerable<T> GetCommonElements(Hierarchy<T> other)
        {
            foreach (var element in _allElements)
            {
                if (other.Contains(element.Value.Value))
                {
                    yield return element.Value.Value;
                }
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            Queue<Node<T>> visitedElements = new Queue<Node<T>>();
            visitedElements.Enqueue(_root);
            while (visitedElements.Count > 0)
            {
                var node = visitedElements.Dequeue();
                yield return node.Value;
                foreach (var child in node.Children)
                {
                    visitedElements.Enqueue(child);
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private void ContainsOrThrowException(T element)
        {
            if (!Contains(element)) throw new ArgumentException();
        }
    }
}