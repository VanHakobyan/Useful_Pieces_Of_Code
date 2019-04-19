using System;
using System.Text;

namespace DoubleLinkedList
{
    /// <summary>
    /// Doubly linked list
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class DoubleLinkedListGeneric<T>
    {
        /// <summary>
        /// Head node
        /// </summary>
        public Node<T> Head { get; set; }

        /// <summary>
        /// Property which returns if list is empty
        /// </summary>
        public bool IsEmpty => Head is null;

        /// <summary>
        /// Count of list
        /// </summary>
        public int Count { get; private set; } = 0;

        /// <summary>
        /// Ctor
        /// </summary>
        public DoubleLinkedListGeneric()
        {
            Head = null;
        }

        /// <summary>
        /// Insert element in the beginning of the list
        /// </summary>
        /// <param name="input">input value</param>
        public void InsertBeginning(T input)
        {
            var insertedNode = new Node<T>(input);
            insertedNode.Next = Head;

            if (Head != null)
                Head.Previous = insertedNode;

            Head = insertedNode;

            Count++;
        }

        /// <summary>
        /// Insert element in the end of the list
        /// </summary>
        /// <param name="input">input value</param>
        public void InsertEnd(T input)
        {
            var insertedNode = new Node<T>(input);

            if (IsEmpty)
            {
                Head = insertedNode;
                Count++;
                return;
            }

            var lastNode = GetLastNode();
            lastNode.Next = insertedNode;
            insertedNode.Previous = lastNode;

            Count++;
        }

        /// <summary>
        /// Insert element in the beginning of the list
        /// </summary>
        /// <param name="input">node to add</param>
        public void InsertEnd(Node<T> node)
        {
            if (IsEmpty)
            {
                Head = node;
                Count++;
                return;
            }

            var lastNode = GetLastNode();
            lastNode.Next = node;
            node.Previous = lastNode;

            Count++;
        }

        /// <summary>
        /// Insert element after a specified node
        /// </summary>
        /// <param name="node">node in the list</param>
        /// <param name="value">value of the node to add</param>
        public void InsertAfter(Node<T> node, T value)
        {
            if (node is null)
                return;

            var insertedNode = new Node<T>(value);
            insertedNode.Next = node.Next;
            node.Next = insertedNode;
            insertedNode.Previous = node;

            if (insertedNode.Next != null)
                insertedNode.Next.Previous = insertedNode;

            Count++;
        }

        /// <summary>
        /// Search Node with node 
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        private Node<T> SearchNode(Node<T> n)
        {
            var index = Head;
            while (index != null)
            {
                if (index.Data.Equals(n.Data))
                    break;

                index = index.Next;
            }

            return index ?? null;
        }

        /// <summary>
        /// Tells if node exists in the list
        /// </summary>
        /// <param name="node">node to look for</param>
        /// <returns>true if node exists, false otherwise</returns>
        public bool FindNode(Node<T> node)
        {
            return SearchNode(node) != null;
        }

        /// <summary>
        /// Get length of the list
        /// </summary>
        /// <returns>length of the list</returns>
        public int ListLength()
        {
            return Count;
        }

        /// <summary>
        /// Removes the head of the list
        /// </summary>
        /// <returns>the removed node</returns>
        public Node<T> RemoveBeginning()
        {
            var temp = Head;
            if (Head != null)
            {
                Head = Head.Next;

                if (Head != null)
                    Head.Previous = null;
            }

            Count--;

            return temp;
        }

        /// <summary>
        /// Removes the specified node
        /// </summary>
        /// <param name="n">node to remove</param>
        public void RemoveNode(Node<T> n)
        {
            if (IsEmpty)
                return;

            var node = SearchNode(n);

            if (node is null)
                return;

            if (node.Previous != null)
                node.Previous.Next = node.Next;
            else
                Head = node.Next;

            if (node.Next != null)
                node.Next.Previous = node.Previous;

            Count--;
        }

        /// <summary>
        /// Swap two random nodes
        /// </summary>
        /// <param name="first">first node</param>
        /// <param name="second">second node</param>
        public void SwapNodes(Node<T> first, Node<T> second)
        {
            var temp = second.Data;
            second.Data = first.Data;
            first.Data = temp;
        }

        /// <summary>
        /// Appends another double linked list
        /// </summary>
        /// <param name="list">list to append</param>
        public void AppendLists(DoubleLinkedListGeneric<T> list)
        {
            if (list is null || list.Count == 0)
                return;

            InsertEnd(list.Head);

            Count += list.Count - 1;
        }

        /// <summary>
        /// Print the list
        /// </summary>
        public void PrintList()
        {
            Console.WriteLine(this);
        }

        /// <summary>
        /// ToString override
        /// </summary>
        /// <returns>string representation</returns>
        public override string ToString()
        {
            if (Head is null)
                return "[]";
            else
            {
                var current = Head;
                var sb = new StringBuilder("[");

                while (current != null)
                {
                    if (current.Data != null)
                        sb.Append($"{current.Data}, ");
                    else
                        sb.Append("null, ");

                    current = current.Next;
                }

                sb = sb.Remove(sb.Length - 2, 2);
                sb.Append("]");

                return sb.ToString();
            }
        }

        private Node<T> GetLastNode()
        {
            var temp = Head;

            while (temp.Next != null)
            {
                temp = temp.Next;
            }

            return temp;
        }
    }
}
