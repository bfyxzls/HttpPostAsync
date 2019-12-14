using System;
using System.Threading;
using System.Threading.Tasks;

namespace httpcliettest
{
    class Program
    {
        static void Main(string[] args)
        {
            HttpHelper httpHelper = new HttpHelper((msg) => { Console.WriteLine("结果：" + msg); });
           // httpHelper.Get("http://www.google.com", null);
            httpHelper.Post("http://www.google.com", "value=lind");
            //WebRequest_BeginGetRequeststream.Test();
            Console.ReadKey();
        }

        static void CreateOrder()
        {
            Console.WriteLine("购买订单");
            Thread.Sleep(3000);
        }
    }
}
