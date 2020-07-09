using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _45_Throw
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                for (int i = 1; i < 20; i++)
                {
                    DoSomting(i);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"예외 발생 : {ex.Message}");
                Console.WriteLine($"도움말 링크 : {ex.HelpLink}");   
                Console.WriteLine($"소스 : {ex.Source}");
            }
            finally
            {
                Console.WriteLine("에러가 있든 없든 수행합니다.");
            }
        }

        private static void DoSomting(int arg)
        {
            if (arg <= 10)
                Console.WriteLine($"arg = {arg}");
            else
                //throw new Exception($"{arg}는 10보다 큽니다.");
                throw new Exception($"arg({arg})는 10보다 큽니다.")
                {
                    HelpLink = "URL",
                    Source = "45_Throw - Make Error"
                };
        }
    }
}
