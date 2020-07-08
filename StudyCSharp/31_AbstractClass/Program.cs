using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _31_AbstractClass
{
    abstract class AbstractBase
    {
        protected void ProtectedMethodA()
        {
            Console.WriteLine("AbstractBase.ProtectedMethodA");
        }
        public void publicMethodA()
        {
            Console.WriteLine("AbstractBase.publicMethodA");
        }
        public abstract void AbstractMethodA();
    }
    class Derived : AbstractBase
    {
        public override void AbstractMethodA()
        {
            Console.WriteLine("Derived.AbstractMethodA");
            ProtectedMethodA();
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            AbstractBase obj = new Derived();
            obj.AbstractMethodA();
            obj.publicMethodA();
        }
    }
}
