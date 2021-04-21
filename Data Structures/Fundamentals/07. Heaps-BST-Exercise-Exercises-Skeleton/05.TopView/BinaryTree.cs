namespace _05.TopView
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class BinaryTree<T> : IAbstractBinaryTree<T>
        where T : IComparable<T>
    {
        public BinaryTree(T value
            , BinaryTree<T> leftChild
            , BinaryTree<T> rightChild)
        {
            this.Value = value;
            this.LeftChild = leftChild;
            this.RightChild = rightChild;
        }

        public T Value { get; set; }

        public BinaryTree<T> LeftChild { get; set; }

        public BinaryTree<T> RightChild { get; set; }

        public List<T> TopView()
        {
            var offsetToValueLevel = new SortedDictionary<int, KeyValuePair<T, int>>();

            FillDicDfs(this, 0, 1, offsetToValueLevel);

            return offsetToValueLevel.Values.Select(kvp => kvp.Key).ToList();
        }

        private void FillDicDfs(BinaryTree<T> subTree, int offset, int level, 
            SortedDictionary<int, KeyValuePair<T, int>> offsetToValueLevel)
        {
            if (subTree == null) return;

            if (!offsetToValueLevel.ContainsKey(offset))
            {
                offsetToValueLevel.Add(offset, new KeyValuePair<T, int>(subTree.Value, level));
            }

            if (level < offsetToValueLevel[offset].Value)
            {
                offsetToValueLevel[offset] = new KeyValuePair<T, int>(subTree.Value,level);
            }

            this.FillDicDfs(subTree.LeftChild, offset -1,level + 1, offsetToValueLevel);
            this.FillDicDfs(subTree.RightChild, offset + 1,level + 1, offsetToValueLevel);
        }
    }
}
