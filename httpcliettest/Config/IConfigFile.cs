using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fuck.Core.Config
{
    public interface IConfigFile
    {
        /// <summary>
        /// 通过配置文件里的key获取它的值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        KeyValue Get(string key);
    }
}
