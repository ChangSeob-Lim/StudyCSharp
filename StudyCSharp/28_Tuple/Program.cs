using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Cache;
using System.Text;
using System.Threading.Tasks;

namespace _28_Tuple
{
    class Program
    {
        static void Main(string[] args)
        {
            var a = ("슈퍼맨", 56, "크립톤");
            Console.WriteLine($"{a.Item1.GetType()}, {a.Item2.GetType()}, {a.Item3.GetType()}");
            Console.WriteLine($"{a.Item1}, {a.Item2}, {a.Item3}");

            var b = (Name: "임창섭", Age: 26, Home: "부산");
            Console.WriteLine($"{b.Name}, {b.Age}, {b.Item3}");
            b = a;
            Console.WriteLine($"{b.Name}, {b.Age}, {b.Home}");

            var (name, age, home) = GetInfo();
            Console.WriteLine($"{name}, {age}, {home}");
        }

        static Tuple<string, int, string> GetInfo()
        {
            return new Tuple<string, int, string>("섭", 26, "BUSAN");
        }
    }
}
