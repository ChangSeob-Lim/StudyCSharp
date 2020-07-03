using static System.Console;

namespace HelloApp
{
    class Program
    {
        static void Main(string[] args)
        {
            if(args.Length == 0)
            {
                WriteLine("ex: HelloApp.exe <이름>");
                return;
            }

            // WriteLine("Hello, {0}!", args[0]);
            WriteLine($"Hello, {args[0]}!");
        }
    }
}
