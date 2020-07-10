using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _53_UsingFunc
{
    class Program
    {
        static void Main(string[] args)
        {
            #region Func 예제

            Func<int> func1 = () => 10; // == { return 10; }; // <out>
            Console.WriteLine($"func1() = {func1()}");
            Console.WriteLine($"Type of func1 = {func1}");

            Func<int, int> func2 = (int x) => x * 2; // == { return x * 2; }; // <input, out>
            Console.WriteLine($"func2() = {func2(4)}");
            Console.WriteLine($"Type of func2 = {func2}");

            try
            {
                Func<double, double, int> func3 = (x, y) => // <input, input, out>
                {
                    if (y == 0)
                    {
                        throw new Exception($"Divide by zero!\nYour input data -> x = {x}, y = {y}");
                    }
                    return (int)(x / y);
                };

                Console.WriteLine($"func3() = {func3(22, 7)}");
                Console.WriteLine($"Type of func3 = {func3}");
                Console.WriteLine($"func3() = {func3(22, 0)}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error : {ex.Message}");
            }

            #endregion

            #region Action 예제

            Action act1 = () => { Console.WriteLine("act1()"); };
            act1();

            int result = 0;
            Action<int> act2 = (x) => result = x * x;
            act2(5);
            Console.WriteLine($"result = {result}");

            Action<double, double> act3 = (x, y) =>
            {
                double pi = x / y;
                Console.WriteLine($"Action<T1, T2>({x}, {y}) = {pi}");
            };
            act3(22.0, 7.0);

            #endregion
        }
    }
}
