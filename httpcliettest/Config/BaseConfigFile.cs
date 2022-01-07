using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fuck.Core.Config
{
    public abstract class BaseConfigFile : IConfigFile
    {
        protected List<KeyValue> keyValues = new List<KeyValue>();
        protected string configFilePath;
        public BaseConfigFile(string file)
        {
            configFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, string.Format(@"Configs\\{0}.json", file));
        }

        public abstract KeyValue Get(string key);

    }
}
