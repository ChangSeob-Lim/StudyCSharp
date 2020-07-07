using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _23_ClassTypeCasting
{
    class Mammal // 포유류
    {
        public void Nurse()
        {
            Console.WriteLine("Nurse()");
        }
    }
    class Dog : Mammal // 개
    {
        public void Bark()
        {
            Console.WriteLine("Bark()");
        }
    }
    class Cat : Mammal // 고양이
    {
        public void Meow()
        {
            Console.WriteLine("Meow()");
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Mammal mammal = new Dog();
            Dog dog;

            if(mammal is Dog)
            {
                //dog = (Dog)mammal;
                dog = mammal as Dog; // 클래스끼리 형변환 할때는 is와 as를 함께 사용하는게 좋다
                dog.Bark();

                Mammal mammal2 = new Cat();
                Cat cat = mammal2 as Cat;
                if (cat != null)
                    cat.Meow();

                Cat cat2 = mammal as Cat;
                if (cat2 != null)
                    cat2.Meow();
                else
                    Console.WriteLine("cat2 is not Cat");
            }
        }
    }
}
