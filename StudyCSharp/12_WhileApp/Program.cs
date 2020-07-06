using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _12_WhileApp
{
    class Program
    {
        static void Main(string[] args)
        {
            // while
            /*int i = 10;
            while(i > 0)
            {
                Console.WriteLine($"i = {i--}");
            }*/

            // foreach
            List<string> strings = new List<string>(); // string 리스트 선언
            strings.Add("Hello"); // 리스트 추가
            strings.Add("Bye");
            strings.Add("Hey~");
            List<string> subs = new List<string> { "Banana", "Strawberry" };
            strings.AddRange(subs);

            var i = 0;
            foreach (var item in strings)
            {
                Console.WriteLine(strings[i]);
                Console.WriteLine($"{++i}번째 item : {item}");
            }
        }
    }
}
