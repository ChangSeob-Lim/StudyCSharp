using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _33_PropertyInterface
{
    interface INamedValue
    {
        string Name { get; set; }
        string Value { get; set; }
        string GetOhterValue();
    }

    abstract class Product
    {
        private static int Serial = 0;

        public string SerialID
        {
            get { return String.Format($"{Serial++:d5}"); }
        }

        abstract public DateTime productDate { get; set; }
    }

    class Myproduct : Product
    {
        public override DateTime productDate { get; set; }
    }

    class NamedValue : INamedValue
    {
        public string Name { get; set; }
        public string Value { get; set; }

        public string GetOhterValue()
        {
            return "OhterValue";
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            NamedValue name = new NamedValue
            {
                Name = "이름",
                Value = "임창섭"
            };

            NamedValue height = new NamedValue
            {
                Name = "키",
                Value = "175cm"
            };

            NamedValue weight = new NamedValue
            {
                Name = "몸무게",
                Value = "75kg"
            };

            Console.WriteLine($"{name.Name} : {name.Value}");
            Console.WriteLine($"{height.Name} : {height.Value}");
            Console.WriteLine($"{weight.Name} : {weight.Value}");
        }
    }
}
