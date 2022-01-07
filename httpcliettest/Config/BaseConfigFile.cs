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
        protected static List<KeyValue> keyValues = null;
        public static FileSystemWatcher watcher = new FileSystemWatcher();
        protected static bool isChanged;
        protected string configFilePath;
        public BaseConfigFile(string file)
        {
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

        public abstract KeyValue Get(string key);

    }
}
