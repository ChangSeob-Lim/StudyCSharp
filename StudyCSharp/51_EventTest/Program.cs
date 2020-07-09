using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _51_EventTest
{
    delegate void EvetHandler(string message);

    class MyNotifier
    {
        public event EvetHandler SomethingHappened;

        public void DoSimething(int number)
        {
            int temp = number % 10;

            if (temp != 0 && temp % 3 == 0)
                SomethingHappened(String.Format($"{number} : 짝"));
        }
    }

    class Program
    {
        static public void MyHandler(string message)
        {
            Console.WriteLine(message);
        }

        static void Main(string[] args)
        {
            MyNotifier notifier = new MyNotifier();
            notifier.SomethingHappened += new EvetHandler(MyHandler);

            for (int i = 0; i < 30; i++)
            {
                notifier.DoSimething(i);
            }
        }
    }
}
