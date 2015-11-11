using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Task.BinaryTree {
    public sealed class BinaryTree<T> : IEnumerable<T> {
        private class Node<TValue> {
            public TValue Value { get; private set; }
            public Node<TValue> LeftNode { get; set; }  
            public Node<TValue> RightNode { get; set; }
            public Node(TValue value) {
                Value = value;
            }   
        }

        private Node<T> m_Root;
        public IComparer<T> Comparer { get; set; }
        public int Count { get; private set; }

        #region Constructors
        public BinaryTree() : this(Comparer<T>.Default) { }

        public BinaryTree(IComparer<T> comparer) {
            if(comparer == null)
                throw new ArgumentException($"Argument {nameof(comparer)} is null");
            Comparer = comparer;
        }

        public BinaryTree(IEnumerable<T> collection) : this(collection, Comparer<T>.Default) { }

        public BinaryTree(IEnumerable<T> collection, IComparer<T> comparer) : this(comparer) {
            AddRange(collection);
        }
        #endregion

        #region Public Methods

        public void AddRange(IEnumerable<T> collection) {
            foreach(var value in collection) {
                Add(value);
            }
        }

        public void Add(T item) {
            var node = new Node<T>(item);
            if(m_Root == null)
                m_Root = node;
            else {
                Node<T> current = m_Root, parent = null;
                while(current != null) {
                    parent = current;
                    current = Comparer.Compare(item, current.Value) < 0 ? current.LeftNode : current.RightNode;
                }
                if(Comparer.Compare(item, parent.Value) < 0)
                    parent.LeftNode = node;
                else
                    parent.RightNode = node;
            }
            ++Count;
        }

        public IEnumerable<T> Inorder() => Inorder(m_Root);
        public IEnumerable<T> Preorder() => Preorder(m_Root); 
        public IEnumerable<T> Postorder() => Postorder(m_Root); 



        public BinaryTree<T> Balance() {
            /*T[] array = this.ToArray();
            Array.Sort(array, Comparer);
            T temp = array[0];
            array[0] = array[array.Length/2];
            array[array.Length/2] = temp;
            return new BinaryTree<T>(array);*/
            throw new NotImplementedException();
        }

        public override string ToString() {
            StringBuilder sb = new StringBuilder();
            sb.Append($"Print BinaryTree. Size: {Count}\n");
            PrintTree(m_Root, sb, 0);
            sb.Append("End of PrintTree\n");
            return sb.ToString();
        }

        #endregion

        #region Private Methods

        private void PrintTree(Node<T> node, StringBuilder sb, int level) {
            if(node != null) {
                PrintTree(node.LeftNode, sb, level + 1);
                for(int i = 0; i < level; i++)
                    sb.Append("   ");
                sb.Append($"{node.Value}\n");
                PrintTree(node.RightNode, sb, level + 1);
            }
        }

        public IEnumerator<T> GetEnumerator() => Inorder().GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        #endregion

        #region PrivateMethod
        private IEnumerable<T> Inorder(Node<T> node) {
            if (node == null) yield break;
            foreach(var n in Inorder(node.LeftNode)) 
                yield return n;
            
            yield return node.Value;
            foreach (var n in Inorder(node.RightNode)) 
                yield return n;
        }

        private IEnumerable<T> Preorder(Node<T> node) {
            if(node == null) yield break;
            yield return node.Value;
            foreach(var n in Preorder(node.LeftNode)) 
                yield return n;
            
            foreach (var n in Preorder(node.RightNode)) 
                yield return n;
        }

        private IEnumerable<T> Postorder(Node<T> node) {
            if(node == null) yield break;

            foreach (var n in Postorder(node.LeftNode))
                yield return n;

            foreach (var n in Postorder(node.RightNode))
                yield return n;
            yield return node.Value;
        } 
        #endregion
    }
}
