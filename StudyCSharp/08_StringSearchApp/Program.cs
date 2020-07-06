using System;
using System.Globalization;
using static System.Console;

namespace _08_StringSearchApp
{
    class Program
    {
        static void Main(string[] args)
        {
            string greeting = "Good Morning!";
            Console.WriteLine(greeting);

            // 문자열 안에서 찾기
            WriteLine("----------------------------------------------");

            // IndexOf() -> 원하는 문자(문자열)의 인덱스 위치 출력, 없으면 -1 출력
            Console.WriteLine($"IndexOf 'o' : {greeting.IndexOf('o')}");
            Console.WriteLine($"IndexOf 'Good' : {greeting.IndexOf("Good")}");
            // LastIndexOf() -> 마지막에 나오는 인덱스 위치 출력
            Console.WriteLine($"LastIndexOf 'o' : {greeting.LastIndexOf('o')}");
            Console.WriteLine($"LastIndexOf 'Good' : {greeting.LastIndexOf("Good")}");
            // StartWith()
            WriteLine($"StartWith 'Good' : {greeting.StartsWith("Good")}");
            WriteLine($"StartWith 'Morning' : {greeting.StartsWith("Morning")}");
            // EndsWith()
            WriteLine($"EndsWith 'Good' : {greeting.EndsWith("Good")}");
            WriteLine($"EndsWith 'Morning' : {greeting.EndsWith("Morning")}");
            // Contains()
            WriteLine($"Contains 'Good' : {greeting.Contains("Good")}");
            WriteLine($"Contains 'Evening' : {greeting.Contains("Evening")}");
            // Replace()
            WriteLine($"Replace 'Morning' to 'Evening' : " +
                      $"{greeting.Replace("Morning", "Evening")}");
            
            //if(greeting.Contains("Morning"))
            //{
            //    greeting = greeting.Replace("Morning", "Evening");
            //    WriteLine($"Replace greeting? : {greeting}");
            //}
            
            WriteLine("----------------------------------------------");

            // 문자열 변형하기
            WriteLine("----------------------------------------------");

            // ToLower() 소문자
            WriteLine($"ToLower : {greeting.ToLower()}");
            // ToUpper() 대문자
            WriteLine($"ToUpper : {greeting.ToUpper()}");
            // Insert()
            WriteLine($"Insert : {greeting.Insert(greeting.IndexOf("Morning") - 1, " Very")}");
            // Remove()
            WriteLine($"Remove : {"I Don't Love You.".Remove(2, 6)}");
            // Trim()
            WriteLine($"Trim : '{" No Space ".Trim()}'");
            WriteLine($"TrimStart : '{" No Space ".TrimStart()}'");
            WriteLine($"TrimEnd : '{" No Space ".TrimEnd()}'");

            WriteLine("----------------------------------------------");

            string codes = "MSFT,GOOG,AMZN,AAPL, RHT";

            // 문자열 분할하기
            WriteLine("----------------------------------------------");

            // Split()
            var result = codes.Split(',');
            foreach (var item in result)
            {
                WriteLine($"Split each item : {item}");
            }
            // SubString
            WriteLine($"SubString : {greeting.Substring(0, 4)}");
            WriteLine($"SubString : {greeting.Substring(5)}");

            WriteLine("----------------------------------------------");

            // 문자열 서식 맞추기
            WriteLine("----------------------------------------------");

            // Format()
            WriteLine(string.Format("{0}DEF", "ABC"));
            WriteLine(string.Format("{0, -10}DEF", "ABC")); // {a, b, c} -> 첨자, 맞춤, 서식 문자열
            WriteLine(string.Format("{0, 10}DEF", "ABC"));

            DateTime dt = DateTime.Now;
            WriteLine(dt);
            //WriteLine(string.Format("{0:yyyy-MM-dd HH:mm:ss}", dt)); // 일반적인 포맷 형식
            WriteLine(string.Format($"{dt:yyyy-MM-dd HH:mm:ss}"));
            WriteLine(string.Format("{0:d/M/yyyy HH:mm:ss}", dt)); // 미국에서 주로 사용하는 포맷 형식
            WriteLine(string.Format("{0:yyyy-MM}", dt));
            WriteLine(string.Format("{0:y yy yyy yyyy}", dt)); // 20 20 2020 2020(2020-07-06 기준)
            WriteLine(string.Format("{0:M MM MMM MMMM}", dt)); // 7 07 7 7월(2020-07-06 기준)
            WriteLine(string.Format("{0:d dd ddd dddd}", dt)); // 6 06 월 월요일(2020-07-06 기준)
            WriteLine(dt.ToString("yyyy-MM-dd HH:mm:ss", new CultureInfo("ko-KR")));
            WriteLine(dt.ToString("d/M/yyyy HH:mm:ss", new CultureInfo("en-US")));

            decimal mvalue = 27000000m;
            //WriteLine(string.Format("price : {0:C}", mvalue));
            //WriteLine(string.Format($"price : {mvalue:C}"));
            WriteLine(mvalue.ToString("C"));
            WriteLine(mvalue.ToString("C", new CultureInfo("un-US")));
            WriteLine(mvalue.ToString("C", new CultureInfo("fr-FR")));
            WriteLine(mvalue.ToString("C", new CultureInfo("ja-JP")));

            WriteLine("----------------------------------------------");
        }
    }
}
