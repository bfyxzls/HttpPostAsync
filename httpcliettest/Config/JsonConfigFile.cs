using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fuck.Core.Config
{
    public class JsonConfigFile : BaseConfigFile
    {
        public JsonConfigFile(string file) : base(file)
        {
            if (keyValues == null || isChanged)
            {
                keyValues = SerializationHelper.DeserializeFromJson<List<KeyValue>>(configFilePath);
            }
        }

        public override KeyValue Get(string key)
        {
            return keyValues.Where(i => i.key.Equals(key)).FirstOrDefault();
        }
    }
}
