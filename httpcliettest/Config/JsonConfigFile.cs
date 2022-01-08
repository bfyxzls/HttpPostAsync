using System.Collections.Generic;

namespace Fuck.Core.Config
{
    public class JsonConfigFile : BaseConfigFile
    {
        public JsonConfigFile(string file) : base(file)
        {
            if (!keyValues.ContainsKey(file) || isChanged)
            {
                keyValues.TryAdd(file, SerializationHelper.DeserializeFromJson<List<KeyValue>>(configFilePath));
            }
        }

    }
}
