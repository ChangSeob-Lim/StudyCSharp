using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _49_DelegateChain
{
    delegate void Notify(string message);

    class Notifier
    {
        public Notify EventOccred;
    }

    class EventListener
    {
        private string name;

        public EventListener(string name)
        {
            this.name = name;
        }

        public void SomethingHappend(string message)
        {
            Console.WriteLine($"{name}.SomethingHappend : {message}");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Notifier notifier = new Notifier();
            EventListener listener1 = new EventListener("Listener1");
            EventListener listener2 = new EventListener("Listener2");
            EventListener listener3 = new EventListener("Listener3");

            notifier.EventOccred += listener1.SomethingHappend;
            notifier.EventOccred += listener2.SomethingHappend;
            notifier.EventOccred += listener3.SomethingHappend;
            notifier.EventOccred("You're got mail.");
            Console.WriteLine();

            notifier.EventOccred -= listener2.SomethingHappend;
            notifier.EventOccred("Download complete.");
            Console.WriteLine();
        }
    }
}
