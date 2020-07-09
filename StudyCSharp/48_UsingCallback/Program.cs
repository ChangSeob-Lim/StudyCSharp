using System;

namespace _48_UsingCallback
{
    delegate int Compare<T>(T a, T b);
    class Program
    {
        static int AscendCompare<T>(T a, T b) where T : IComparable<T>
        {
            /*if (a > b)
                return 1;
            else if (a == b)
                return 0;
            else
                return -1;*/
            return a.CompareTo(b);
        }
        static int DescendCompare<T>(T a, T b) where T : IComparable<T>
        {
            /*if (a < b)
                return 1;
            else if (a == b)
                return 0;
            else
                return -1;*/
            return a.CompareTo(b) * -1;
        }
        static void BubbleSort<T>(T[] Dataset, Compare<T> comparer)
        {
            T temp;

            for (int i = 0; i < Dataset.Length - 1; i++)
            {
                for (int j = 0; j < Dataset.Length - (i + 1); j++)
                {
                    if(comparer(Dataset[j], Dataset[j+1])>0)
                    {
                        temp = Dataset[j + 1];
                        Dataset[j+1] = Dataset[j];
                        Dataset[j] = temp;
                    }
                }
            }
        }

        static void Main(string[] args)
        {
            int[] array = { 3, 7, 4, 2, 10 };
            Console.WriteLine("Sorting asending!");
            BubbleSort<int>(array, new Compare<int>(AscendCompare));

            foreach (var item in array)
            {
                Console.Write($"{item}, ");
            }Console.WriteLine();

            Console.WriteLine("Sorting desending!");
            BubbleSort<int>(array, new Compare<int>(DescendCompare));

            foreach (var item in array)
            {
                Console.Write($"{item}, ");
            }Console.WriteLine();
        }
    }
}
