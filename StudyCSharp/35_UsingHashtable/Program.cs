using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _35_UsingHashtable
{
    class Program
    {
        static void Main(string[] args)
        {
            Hashtable ht = new Hashtable();

            ht["이름"] = "임창섭";
            ht["주소"] = "부산광역시";
            ht["전화번호"] = "010-1234-5678";
            ht["키"] = 175.0;
            ht["결혼여부"] = false;

            Console.WriteLine($"{ht["키"]:0.0}");
            Console.WriteLine($"{ht["결혼여부"]}");
        }
    }
}
