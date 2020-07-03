using System;
using static System.Console;

namespace IntegralType
{
    class Program
    {
        static void Main(string[] args)
        {
            //각 데이터 형식의 최댓값 최솟값을 알수 있음 -> MaxValue, MinValue
            sbyte a = sbyte.MinValue;
            byte b = byte.MaxValue;
            WriteLine($"a={a}, b={b}");

            short c = short.MinValue;
            ushort d = ushort.MaxValue;
            WriteLine($"c={c}, d={d}");


            int e = int.MinValue;
            uint f = int.MaxValue;
            WriteLine($"d={e}, f={f}");

            long g = long.MinValue;
            ulong h = long.MaxValue;
            WriteLine($"g={g}, h={h}");

            //숫자 참조 값이 길때 100_000_000_000 -> _로 자리 구분 가능
            long i = 100_000_000_000;
            WriteLine($"i={i}");

            byte j = 240;   //10진수
            WriteLine($"j = {j}");
            byte k = 0b1111_0000;   //2진수
            WriteLine($"b = {k}");
            byte l = 0xF0;  //16진수
            WriteLine($"c = {l}");

            //오버플로우
            byte n = byte.MaxValue;
            n += 1;
            WriteLine($"n = {n}");
            n += 1;
            WriteLine($"n = {n}");

            float o = float.MaxValue;
            WriteLine($"o = {o}");
            double p = double.MaxValue;
            WriteLine($"p = {p}");
        }
    }
}
