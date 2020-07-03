using static System.Console;

namespace HelloApp
{
    class Program
    {

        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                WriteLine("ex: HelloApp.exe <이름>");
                return;
            }

            //Console.WriteLine("Hello, {0}!", args[0]);    //옛날 쓰던방식 - > 상대적 손이 많이감
            WriteLine($"Hello, {args[0]}!");    //요즘 쓰는 방식 -> 상대적 간편
        }
    }
}
