using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        public class Human
        {
            public int Age;
            private readonly string Name;

            public Human(int age, string name)
            {
                Age = age;
                Name = name;
            }
            public override string ToString()
            {
                return $"Name - {Name} , Age - {Age}";
            }
        }

        private static void ChangeHumanByValue(Human human)
        {
            human.Age = 27;
            human = new Human(70, "Van");
            Console.WriteLine(human.ToString());
        }

        private static void ChangeHumanByRef(ref Human human)
        {
            human.Age = 25;
            human = new Human(27, "Aram");
            Console.WriteLine(human.ToString());
        }
        static void Main(string[] args)
        {
            var human = new Human(21, "Van");
            Console.WriteLine(human.ToString()); // Van  21
            ChangeHumanByValue(human);           // Van  70
            Console.WriteLine(human.ToString()); // Van  27
            ChangeHumanByRef(ref human);         // Aram 27
            Console.WriteLine(human.ToString()); // Aram 27
        }
    }
}
