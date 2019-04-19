using System;

namespace DoubleLinkedList
{
    class Program
    {
        static void Main(string[] args)
        {
            var helper = new Helper();
            var list = new DoubleLinkedListGeneric<int>();
            list.InsertBeginning(helper.GenerateRandomInt());
            list.InsertEnd(helper.GenerateRandomInt());
            list.InsertBeginning(helper.GenerateRandomInt());
            list.InsertEnd(helper.GenerateRandomInt());
            list.InsertAfter(list.Head, helper.GenerateRandomInt());

            var head = list.FindNode(list.Head);
            Console.WriteLine($"head={head}");

            var notExists = list.FindNode(new Node<int>(int.MinValue));
            Console.WriteLine($"Is exist {notExists}");

            var removedFromBeginning = list.RemoveBeginning();
            Console.WriteLine($"removed from beginning {removedFromBeginning.Data}");

            list.RemoveNode(list.Head);
            list.PrintList();

            var otherList = new DoubleLinkedListGeneric<int>();
            otherList.InsertEnd(helper.GenerateRandomInt());
            otherList.InsertAfter(otherList.Head, helper.GenerateRandomInt());
            otherList.InsertEnd(helper.GenerateRandomInt());
            list.AppendLists(otherList);
            list.PrintList();

            list.SwapNodes(list.Head, otherList.Head);

            Console.ReadKey();
        }

    }
}
