using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _44_KillingProgram
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] arr = { 1, 2, 3 };
            int x = 100, y = 0;
            string sValue = "Make Error!";
            try
            {
                for (int i = 0; i < 3; i++)
                {
                    Console.WriteLine(arr[i]);
                }

                y = 1;
                Console.WriteLine(x / y);

                int val = int.Parse(sValue);
                Console.WriteLine(val);
            }
            catch (IndexOutOfRangeException ex)
            {
                Console.WriteLine($"예외 발생 : {ex.Message}");
                //ex.HelpLink = "https://docs.microsoft.com/ko-kr/dotnet/api/system.indexoutofrangeexception?view=netcore-3.1";
                //Console.WriteLine($"도움말 : {ex.HelpLink}");
                //Console.WriteLine(ex.StackTrace);
                //Console.WriteLine(ex.Source);
            }
            catch (DivideByZeroException ex)
            {
                Console.WriteLine($"예외 발생 : {ex.Message}");
            }
            catch(Exception ex)
            {
                Console.WriteLine($"예외 발생 : {ex.Message}");
            }
            finally
            {
                Console.WriteLine($"종료");
            }
        }
    }
}
