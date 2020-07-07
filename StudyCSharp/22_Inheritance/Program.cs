using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _22_Inheritance
{
    class Parent
    {
        protected string Name { get; set; }

        public Parent(string name)
        {
            Name = name;
            Console.WriteLine($"{Name}.Base()");
        }

        public void BaseMethod()
        {
            Console.WriteLine($"{Name}.BaseMethod()");
        }
    }
    class Child : Parent
    {
        public Child(string name) : base(name)
        {
            Console.WriteLine($"{this.Name}.Child()");
        }

        public void DerivedMethod()
        {
            base.BaseMethod();
            Console.WriteLine($"{Name}.DerivedMethod()");
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("---------------------------");
            Parent p = new Parent("Parent Class");
            p.BaseMethod();
            Console.WriteLine("---------------------------");

            Child c = new Child("Child Class");
            c.BaseMethod();
            Console.WriteLine("---------------------------");
            c.DerivedMethod();
            Console.WriteLine("---------------------------");
        }
    }
}
