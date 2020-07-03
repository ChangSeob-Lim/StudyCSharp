using System;

namespace _05_StringNumberConverion
{
    class Program
    {
        static void Main(string[] args)
        {
            //정수형 -> 문자형
            int a = 12345;
            string b = a.ToString();
            Console.WriteLine($"b = {b}");

            //실수형 -> 문자형
            float c = 3.141592f;
            string d = c.ToString();
            Console.WriteLine($"d = {d}");
            Console.WriteLine($"d = {d}");

            //문자형 -> 정수형
            string e = "123456";
            int f = Convert.ToInt32(e);
            f = int.Parse(e);
            Console.WriteLine($"f = {f}");

            //문자형 -> 실수형
            string g = "3.141592";
            float h = float.Parse(g);
            Console.WriteLine($"h = {h}");

            //문자형 -> 실수형or정수형 변환 오류
            string i = "123456*";
            int j = 0;
            bool result = int.TryParse(i, out j);
            Console.WriteLine($"result = {result}");
            //실 사용예 int -> TryParse
            if (int.TryParse(i, out j))
            {
                Console.WriteLine($"result = {result}");
            }
            //실 사용예 float -> TryParse
            string k = "3:141592";
            float l;
            if (float.TryParse(k, out l))
            {
                Console.WriteLine($"k = {k}");
            }
            else
            {
                Console.WriteLine("k 변환시 에러발생, 문자열 확인 요망");
            }

            //상수(const) = 변할수 없는값, 주로 대문자 씀
            const int FIXVAL = 20;
            //FIXVAL = 21; 상수 값 변환시 에러
        }
    }
}
