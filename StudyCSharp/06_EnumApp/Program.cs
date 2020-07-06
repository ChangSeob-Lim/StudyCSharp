using System;

namespace _06_EnumApp
{
    class Program
    {
        enum DialogResult
        {
            // 열거형 선언, 사용방법
            YES = 10,
            NO = 20,
            CANCEL = 30,
            CONFIRM = 40,
            OK = 50
        }
        static void Main(string[] args)
        {
            //Console.WriteLine(DialogResult.OK);
            //Console.WriteLine((int)DialogResult.OK);
            DialogResult result = DialogResult.YES;

            if (result == DialogResult.YES)
                Console.WriteLine("YES를 선택했습니다.");
        }
    }
}
