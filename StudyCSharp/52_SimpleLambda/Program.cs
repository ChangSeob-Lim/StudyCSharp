using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _52_SimpleLambda
{
    class Program
    {
        delegate int Calculate(int a, int b);
        delegate string Concatnate(string[] args);

        static int Plus(int a, int b)
        {
            return a + b;
        }

        static string strConcat(string[] arr)
        {
            string result = string.Empty; // "" = string.Empty
            foreach (var item in arr)
            {
                result += " " + item;
            }

            return result;
        }

        static void Main(string[] args)
        {
            #region region을 사용하여 코드 정리하기(Calculate 부분)

            // 대리자 사용하기
            Calculate calc = new Calculate(Plus);
            Console.WriteLine(calc(3, 4));

            // 람다식 사용하기
            calc = (a, b) => a + b;
            Console.WriteLine(calc(3, 4));

            #endregion

            #region region을 사용하여 코드 정리하기(Concat 부분)

            // 람다식 사용하기
            Concatnate concat = (arr) =>
            {
                string result = string.Empty; // "" = string.Empty
                foreach (var item in arr)
                {
                    result += " " + item;
                }

                return result;
            };

            Console.WriteLine(concat(args));

            // 대리자 사용하기
            concat = new Concatnate(strConcat);
            Console.WriteLine(concat(args));

            #endregion
        }
    }
}
