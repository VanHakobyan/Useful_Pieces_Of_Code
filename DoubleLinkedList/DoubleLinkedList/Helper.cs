using System;

namespace DoubleLinkedList
{
    /// <summary>
    /// Helper class
    /// </summary>
    public class Helper
    {
        private int _l;
        private int _r;

        /// <summary>
        /// Generates a random integer number
        /// </summary>
        /// <returns>random number</returns>
        public int GenerateRandomInt()
        {
            return new Random().Next(_l += 5, 1000 + (_r += 5));
        }
    }
}
