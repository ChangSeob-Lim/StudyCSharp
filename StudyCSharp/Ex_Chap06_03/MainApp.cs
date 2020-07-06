using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex_Chap06_03
{
    class MainApp
    {
        public static void Main(string[] args)
        {
            int a = 3;
            int b = 4;
            int resultA = 0;

            Plus(a, b, out resultA);

            Console.WriteLine($"{a} + {b} = {resultA}");

            double x = 2.4;
            double y = 3.1;
            double resultB = 0;

            Plus(x, y, out resultB);

            Console.WriteLine($"{x} + {y} = {resultB}");
        }

        public static void Plus(int a, int b, out int c)
        {
            c = a + b;
        }

        public static void Plus(double x, double y, out double z)
        {
            z = x + y;
        }
    }
}
