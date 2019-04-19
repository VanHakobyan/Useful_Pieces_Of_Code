namespace DoubleLinkedList
{
    /// <summary>
    /// Node class
    /// </summary>
    /// <typeparam name="T">node data type</typeparam>
    public class Node<T>
    {
        /// <summary>
        /// node value
        /// </summary>
        public T Data { get; set; }

        /// <summary>
        /// reference to previous node
        /// </summary>
        public Node<T> Previous { get; set; }

        /// <summary>
        /// reference to next node
        /// </summary>
        public Node<T> Next { get; set; }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="input">input value</param>
        public Node(T input)
        {
            Data = input;
            Previous = null;
            Next = null;
        }

        /// <summary>
        /// ToString override
        /// </summary>
        /// <returns>node string representation</returns>
        public override string ToString()
        {
            return this.Data.ToString();
        }
    }
}