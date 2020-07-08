using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _39_CopyingArray
{
    class Program
    {
        static void CopyArray<T>(T[] source, T[] target)
        {
            for (int i = 0; i < source.Length; i++)
            {
                target[i] = source[i];
            }
        }
        static void Main(string[] args)
        {
            //TestProcess();
            int[] source = { 1, 3, 5, 7, 9 };
            int[] target = new int[source.Length];

            CopyArray(source, target); //CopyArray<int>(source, target);
            foreach (var item in target)
            {
                Console.Write($"{item}. ");
            }Console.WriteLine();

            string[] source2 = { "하나", "둘", "셋", "넷" };
            string[] target2 = new string[source2.Length];

            CopyArray(source2, target2); //CopyArray<string>(source2, target2);
            foreach (var item in target2)
            {
                Console.Write($"{item}. ");
            }Console.WriteLine();
        }

        private static void TestProcess()
        {
            Console.WriteLine("TestProcess!");
        }
    }
}
