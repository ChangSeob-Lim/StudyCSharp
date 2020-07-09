using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _47_Delegate
{
    delegate int MyDelegate(int a, int b);

    class Calculator
    {
        public int Plus(int a, int b)
        {
            return a + b;
        }

        public int Minus(int a, int b)
        {
            return a - b;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            // 이전까지의 방식
            Console.WriteLine("일반적인 경우");
            Calculator calc = new Calculator();
            Console.WriteLine(calc.Plus(3, 4));
            Console.WriteLine(calc.Minus(7, 4));

            // 대리자
            Console.WriteLine("\n대리자를 사용할 경우");
            MyDelegate Callback = new MyDelegate(calc.Plus);
            Console.WriteLine(Callback(3, 5));

            Callback = new MyDelegate(calc.Minus);
            Console.WriteLine(Callback(8, 5));
        }
    }
}
