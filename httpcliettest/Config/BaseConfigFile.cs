using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fuck.Core.Config
{
    /// <summary>
    /// 配置文件基类
    /// </summary>
    public abstract class BaseConfigFile : IConfigFile
    {
        /// <summary>
        /// 配置文件列表,key是配置文件名，value是配置文件内容
        /// </summary>
        protected static ConcurrentDictionary<string,List<KeyValue>> keyValues = null;

        /// <summary>
        /// 在类第一次被加载时，对字典进行初始化
        /// </summary>
        static BaseConfigFile()
        {
            keyValues = new ConcurrentDictionary<string, List<KeyValue>>();
        }
        /// <summary>
        /// 对配置文件进行监听，当文件有变化时，会触发相应的事件
        /// </summary>
        public static FileSystemWatcher watcher = new FileSystemWatcher();
        /// <summary>
        /// 是否有变化
        /// </summary>
        protected static bool isChanged;
        /// <summary>
        /// 配置文件地址
        /// </summary>
        protected string configFilePath;
        private string _file;
        public BaseConfigFile(string file)
        {
            this._file = file;
            string configDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, string.Format(@"Configs"));
            configFilePath = Path.Combine(configDir, string.Format(@"{0}.json", file));
            watcher = new FileSystemWatcher(configDir);
            watcher.Filter ="*.json";
            watcher.NotifyFilter = NotifyFilters.LastWrite;
            watcher.IncludeSubdirectories = false;
            watcher.EnableRaisingEvents = true;
            watcher.Changed += Watcher_Changed;
        }

        private void Watcher_Changed(object sender, FileSystemEventArgs e)
        {
            isChanged = true;
            watcher.EnableRaisingEvents = false;
        }

        public virtual KeyValue Get(string key)
        {
            if (keyValues.ContainsKey(_file))
            {
                return keyValues[_file].Where(i => i.key.Equals(key)).FirstOrDefault();
            }
            return null;
        }

    }
}
