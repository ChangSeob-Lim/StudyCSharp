using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _50_AnonymousMethod
{
    delegate int Compare<T>(T a, T b);
    class Program
    {
        static void BubbleSort<T>(T[] Dataset, Compare<T> comparer)
        {
            T temp;

            for (int i = 0; i < Dataset.Length - 1; i++)
            {
                for (int j = 0; j < Dataset.Length - (i + 1); j++)
                {
                    if (comparer(Dataset[j], Dataset[j + 1]) > 0)
                    {
                        temp = Dataset[j + 1];
                        Dataset[j + 1] = Dataset[j];
                        Dataset[j] = temp;
                    }
                }
            }
        }

        static void Main(string[] args)
        {
            int[] array = { 3, 7, 4, 2, 10 };

            Console.WriteLine("Sorting Ascending...");
            BubbleSort<int>(array, delegate (int a, int b)
            {
                return a.CompareTo(b);
            });

            foreach (var item in array)
            {
                Console.Write($"{item}, ");
            }Console.WriteLine();

            Console.WriteLine("Sorting Descending...");
            BubbleSort<int>(array, delegate (int a, int b)
            {
                return a.CompareTo(b) * -1;
            });

            foreach (var item in array)
            {
                Console.Write($"{item}, ");
            }
            Console.WriteLine();
        }
    }
}
