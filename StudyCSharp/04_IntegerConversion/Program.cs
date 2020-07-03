using System;
using static System.Console;

namespace _04_IntegerConversion
{
    class Program
    {
        static void Main(string[] args)
        {
            //작은 타입 - > 큰 타입 = 묵시적 변환(자동)
            sbyte a = sbyte.MinValue;
            WriteLine($"a = {a}");
            int b = a;
            WriteLine($"b = {b}");
            int x = 128;
            WriteLine($"x = {x}");
            //큰타입 -> 작은타입 변환 = 명시적 변환     명시적 변환중 발생 오버플로우는 개발자 책임
            sbyte y = (sbyte)x;
            WriteLine($"y = {y}");
        }
    }
}
