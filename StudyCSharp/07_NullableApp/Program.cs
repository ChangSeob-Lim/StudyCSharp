using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _07_NullableApp
{
    class Program
    {
        static void Main(string[] args)
        {
            int? a = null;
            double? b = null;
            char? c = null;
            string s = null; // string은 원래 null값 허용

            Console.WriteLine(a.HasValue);
            if (a.HasValue)
                Console.WriteLine(a.Value); // null값이라면 에러
            Console.WriteLine(b == null);
            Console.WriteLine(string.IsNullOrEmpty(s)); // (s == null || s == "")
            Console.WriteLine(string.IsNullOrWhiteSpace(s)); // null or "" or " " 세 가지 확인 가능

            b = 3.141592;
            if (b.HasValue)
                Console.WriteLine($"b = {b.Value}");
        }
    }
}
