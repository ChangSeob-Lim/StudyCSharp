using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _41_GenericParameters
{
    class StructArray<T> where T : struct // struct : 값 형식만 사용 가능(int, double...) / class : 참조 형식만 사용 가능(string, class...)
    {
        public T[] Array { get; set; }

        public StructArray(int size)
        {
            Array = new T[size];
        }
    }

    class RefArray<T> where T : class
    {
        public T[] Array { get; set; }

        public RefArray(int size)
        {
            Array = new T[size];
        }
    }

    class Base { }
    class Derived : Base { }
    class BaseArray<U> where U : Base
    {
        public U[] Array { get; set; }

        public BaseArray(int size)
        {
            Array = new U[size];
        }

        public void CopyArray<T>(T[] source) where T : U
        {
            source.CopyTo(Array, 0);
        }
    }

    class Program
    {
        public static T CreatedInstance<T>() where T : new()
        {
            return new T();
        }
        static void Main(string[] args)
        {
            StructArray<int> a = new StructArray<int>(3);
            a.Array[0] = 0;
            //RefArray<string> b = new RefArray<string>(3);
            //RefArray<>

            BaseArray<Base> c = new BaseArray<Base>(3);
            c.Array[0] = new Base();
            c.Array[1] = new Derived();
            c.Array[2] = CreatedInstance<Derived>();

            BaseArray<Derived> d = new BaseArray<Derived>(3);
        }
    }
}
