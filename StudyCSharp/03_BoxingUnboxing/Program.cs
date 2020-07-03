using System;
using static System.Console;

namespace _03_BoxingUnboxing
{
    class Program
    {
        static void Main(string[] args)
        {
            object a = 300;
            int b = (int)a;
            double c = (double)a;
        }
    }
}
