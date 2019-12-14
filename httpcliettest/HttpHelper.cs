using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;

namespace httpcliettest
{

    /// <summary>
    /// 请求数据包
    /// </summary>
    public class RequestState
    {
        public WebRequest request { get; set; }
        public byte[] dataBytes { get; set; }
    }

    /// <summary>
    /// 关于http的调用
    /// </summary>
    public class HttpHelper
    {

        /// <summary>
        /// 方法的委托，参数是一个字符串
        /// </summary>
        private Action<string> action;
        /// <summary>
        /// 线程通知器，线程执行结果通知到其它线程，异步执行
        /// </summary>
        public ManualResetEvent allDone = new ManualResetEvent(false);
        /// <summary>
        /// 初始化需要提供你的方法委托
        /// </summary>
        /// <param name="action"></param>
        public HttpHelper(Action<string> action = null)
        {
            this.action = action;
        }

        /// <summary>
        /// 异步Get请求
        /// </summary>
        /// <param name="url"></param>
        /// <param name="param"></param>
        public void Get(string url, string param)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            HttpWebRequest state = (HttpWebRequest)WebRequest.Create(url);
            state.Timeout = 20000;
            state.BeginGetResponse(new AsyncCallback(Async), state);
            stopwatch.Stop();
            Console.WriteLine("get执行时间：" + stopwatch.ElapsedMilliseconds + "毫秒");
        }
        private void ReadCallback(IAsyncResult asynchronousResult)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            RequestState myRequestState = (RequestState)asynchronousResult.AsyncState;
            WebRequest myWebRequest = myRequestState.request;
            Stream streamResponse = myWebRequest.EndGetRequestStream(asynchronousResult);
            streamResponse.Write(myRequestState.dataBytes, 0, myRequestState.dataBytes.Length);
            streamResponse.Close();
            allDone.Set();
            stopwatch.Stop();
            Console.WriteLine("post参数请求执行时间：" + stopwatch.ElapsedMilliseconds + "毫秒" + DateTime.Now);
        }
        /// <summary>
        /// 异步post请求
        /// </summary>
        /// <param name="url"></param>
        /// <param name="formData"></param>
        public void Post(string url, string formData)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            stopwatch.Stop();
            Console.WriteLine("post执行响应前时间：" + stopwatch.ElapsedMilliseconds + "毫秒" + DateTime.Now);

            request.Timeout = 20000;
            Encoding myEncoding = Encoding.UTF8;
            request.KeepAlive = false;
            request.AllowAutoRedirect = false;
            request.ContentType = "application/x-www-form-urlencoded";
            request.Method = "POST";
            RequestState myRequestState = new RequestState();
            myRequestState.request = request;
            byte[] postData = Encoding.UTF8.GetBytes(formData);
            myRequestState.dataBytes = postData;
            //异步组织数据
            request.BeginGetRequestStream(new AsyncCallback(ReadCallback), myRequestState);
            allDone.WaitOne();


            stopwatch = new Stopwatch();
            stopwatch.Start();
            //异步响应请求结果
            request.BeginGetResponse(new AsyncCallback(Async), request);
            stopwatch.Stop();
            Console.WriteLine("post执行响应后时间：" + stopwatch.ElapsedMilliseconds + "毫秒" + DateTime.Now);
        }


        /// <summary>
        /// 异步GET请求
        /// </summary>
        /// <param name="A_0"></param>
        private void Async(IAsyncResult A_0)
        {
            try
            {
                WebRequest asyncState = (WebRequest)A_0.AsyncState;
                WebResponse response = asyncState.EndGetResponse(A_0);
                HttpWebResponse response2 = (HttpWebResponse)asyncState.GetResponse();
                Stream responseStream = response2.GetResponseStream();
                Encoding encoding = Encoding.GetEncoding("utf-8");
                string content = new StreamReader(responseStream, encoding).ReadToEnd();
                if (response2.StatusCode == HttpStatusCode.OK)
                {
                    //获取返回结果
                    action?.Invoke(content);
                }
                responseStream.Close();
                response2.Close();
                response.Close();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }
        }



    }


}
