using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace _56_BasicThread
{
    class Program
    {
        static void DoSomething()
        {
            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine($"DoSomething: {i}");
                Thread.Sleep(1000);
            }
        }

        static void DoAnything()
        {
            for (int i = 0; i < 15; i++)
            {
                Console.WriteLine($"DoAnything: {i}");
                Thread.Sleep(300);
            }
        }
        static void Main(string[] args)
        {
            Thread thread = new Thread(new ThreadStart(DoSomething));
            Thread thread1 = new Thread(new ThreadStart(DoAnything));

            Console.WriteLine("Starting thread...");
            thread.Start(); // 스레드 시작(DoSomething 호출)
            thread1.Start();

            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine($"Main : {i}");
                Thread.Sleep(500);
            }

            Console.WriteLine("Waiting untill thread stop...");
            thread.Join();
            thread1.Join();

            Console.WriteLine("Done");
        }
    }
}
