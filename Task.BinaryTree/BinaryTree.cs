using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Task.BinaryTree {
    public sealed class BinaryTree<T> : IEnumerable<T> {
        private class Node<TValue> {
            public TValue Value { get; }
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
            try {
                comparer.Compare(default(T), default(T));
            } catch(NullReferenceException) {} catch(ArgumentException ex) {
                throw new ArgumentException($"Type {typeof(T)} doesn't realize interface IComparable or IComparable<{typeof(T)}>", ex);
            }
            Comparer = comparer;
        }

        public BinaryTree(IEnumerable<T> collection) : this(collection, Comparer<T>.Default) { }

        public BinaryTree(IEnumerable<T> collection, IComparer<T> comparer) : this(comparer) {
            AddRange(collection);
        }
        #endregion

        #region Public Methods

        /// <summary>Adds an items to the <see cref="BinaryTree{T}"/></summary>
        /// <param name="collection">The collection to add to the <see cref="BinaryTree{T}"/></param>
        public void AddRange(IEnumerable<T> collection) {
            foreach(var value in collection) {
                Add(value);
            }
        }

        /// <summary>Adds an item to the <see cref="BinaryTree{T}"/></summary>
        /// <param name="item">The object to add to the <see cref="BinaryTree{T}"/></param>
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
                ChangeParent(parent, item, node);
            }
            ++Count;
        }

        public bool Remove(T item) {
            if(m_Root == null) return false;

            Node<T> parent = null;
            var current = Find(item, ref parent);
            if(current == null)
                return false;

            if (current.RightNode == null) 
                ChangeParent(parent, current, current.LeftNode);
            else if (current.RightNode.LeftNode == null) {
                current.RightNode.LeftNode = current.LeftNode;
                ChangeParent(parent, current, current.RightNode);
            } else {
                Node<T> min = current.RightNode.LeftNode, prev = current.RightNode;
                while (min.LeftNode != null) {
                    prev = min;
                    min = min.LeftNode;
                }
                prev.LeftNode = min.RightNode;
                min.LeftNode = current.LeftNode;
                min.RightNode = current.RightNode;

                ChangeParent(parent, current, min);
            }

            --Count;
            return true;
        }

        public void Clear() {
            m_Root = null;
            Count = 0;
        }

        public bool Contains(T item) {
            var current = m_Root;
            while(current != null) {
                var result = Comparer.Compare(item, current.Value);
                if(result == 0)
                    return true;
                current = result < 0 ? current.LeftNode : current.RightNode;
            }
            return false;
        }

        public T MinValue {
            get {
                if (m_Root == null)
                    throw new InvalidOperationException("Tree is empty");
                var current = m_Root;
                while (current.LeftNode != null)
                    current = current.LeftNode;
                return current.Value;
            }
        }
        public T MaxValue {
            get {
                if (m_Root == null)
                    throw new InvalidOperationException("Tree is empty");
                var current = m_Root;
                while (current.RightNode != null)
                    current = current.RightNode;
                return current.Value;
            }
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
            if(m_Root == null)
                sb.Append("Binary tree is empty\n");
            else
                PrintTree(m_Root, sb, 0);
            sb.Append("End of PrintTree\n");
            return sb.ToString();
        }

        public IEnumerator<T> GetEnumerator() => Inorder().GetEnumerator();
        #endregion

        #region PrivateMethod
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        private Node<T> Find(T item, ref Node<T> parent) {
            var current = m_Root;
            while (current != null) {
                var result = Comparer.Compare(item, current.Value);
                if (result == 0)
                    return current;
                parent = current;
                current = result < 0 ? current.LeftNode : current.RightNode;
            }
            return null;
        }

        private void ChangeParent(Node<T> parent, Node<T> current, Node<T> child) {
            if(current == m_Root) {
                m_Root = child;
                return;
            }
            ChangeParent(parent, current.Value, child);
        }

        private void ChangeParent(Node<T> parent, T value, Node<T> child) {
            int result = Comparer.Compare(value, parent.Value);
            if (result < 0)
                parent.LeftNode = child;
            else
                parent.RightNode = child;
        }

        private void PrintTree(Node<T> node, StringBuilder sb, int level) {
            if (node != null) {
                PrintTree(node.LeftNode, sb, level + 1);
                for (int i = 0; i < level; i++)
                    sb.Append("   ");
                sb.Append($"{node.Value}\n");
                PrintTree(node.RightNode, sb, level + 1);
            }
        }

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