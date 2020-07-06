using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace _13_SwapByValue
{
    class Program
    {
        static void Main(string[] args)
        {
            int x = 3;
            int y = 4;

            Console.WriteLine($"x : {x}, y : {y}");

            Swap(ref x, ref y); // ref 참조에 의한 매개 변수 전달

            Console.WriteLine($"x : {x}, y : {y}");
        }

        private static void Swap(ref int a, ref int b)
        {
            int temp = b;
            b = a;
            a = temp;
        }
    }
}
