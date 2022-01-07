using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Fuck.Core.Http
{
    /// <summary>
    /// 将HttpClient做成单例的，不用Using，全局只有一个
    /// 来解决tcp连接不能释放的问题
    /// </summary>
    public class HttpClientFactory
    {
        private static HttpClient _httpClient = null;

        /// <summary>
        /// 静态的构造函数：只能有一个，且是无参数的
        /// 由CLR保证，只有在程序第一次使用该类之前被调用，而且只能调用一次
        /// 说明： keep-alive关键字可以理解为一个长链接，超时时间也可以在上面进行设置，例如10秒的超时时间，当然并发量太大，这个10秒应该会抛弃很多请求
        /// 发送请求的代码没有了using，即这个httpclient不会被手动dispose，而是由系统控制它，当然你的程序重启时，这也就被回收了。
        /// </summary>
        static HttpClientFactory()
        {
            _httpClient = new HttpClient(new HttpClientHandler());
            _httpClient.Timeout = new TimeSpan(0, 0, 10);
            _httpClient.DefaultRequestHeaders.Connection.Add("keep-alive");
        }

        /// <summary>
        /// 对外开放接口
        /// </summary>
        /// <returns></returns>
        public static HttpClient GetHttpClient()
        {
            return _httpClient;
        }
    }
}
