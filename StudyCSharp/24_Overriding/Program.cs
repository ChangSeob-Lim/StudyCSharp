using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _24_Overriding
{
    class ArmorSuite
    {
        public virtual void Initialize()
        {
            Console.WriteLine("Armored");
        }
    }
    class IronMan : ArmorSuite
    {
        public override void Initialize()
        {
            base.Initialize();
            Console.WriteLine("Repulsor Rays Armed");
        }
    }
    class WarMachine : ArmorSuite
    {
        public override void Initialize()
        {
            base.Initialize();
            Console.WriteLine("Double-Barrel Cannos Armed");
            Console.WriteLine("Micro-Rocket Launcher Armed");
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            ArmorSuite armorSuite = new ArmorSuite();
            armorSuite.Initialize();
            Console.WriteLine();

            ArmorSuite ironMan = new IronMan();
            ironMan.Initialize();
            Console.WriteLine();

            ArmorSuite warMachine = new WarMachine();
            warMachine.Initialize();
            Console.WriteLine();
        }
    }
}
