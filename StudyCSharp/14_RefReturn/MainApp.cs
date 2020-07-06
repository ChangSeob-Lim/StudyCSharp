using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _14_RefReturn
{
    class Product
    {
        private int price = 100;

        public void PrintPrice()
        {
            Console.WriteLine($"Price : {price}");
        }

        public ref int GetPrice()
        {
            return ref price;
        }
    }
    class MainApp
    {
        static void Main(string[] args)
        {
            Product carrot = new Product();
            ref int ref_local_price = ref carrot.GetPrice();
            int normal_local_price = carrot.GetPrice();

            carrot.PrintPrice();
            Console.WriteLine($"local_price : {ref_local_price}");
            Console.WriteLine($"normal_price : {normal_local_price}");

            ref_local_price = 3000;
            normal_local_price = 2000;

            carrot.PrintPrice();
            Console.WriteLine($"local_price : {ref_local_price}");
            Console.WriteLine($"normal_price : {normal_local_price}");
        }
    }
}
