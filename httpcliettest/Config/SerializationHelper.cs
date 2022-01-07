using System;
using System.IO;
using System.Text;

namespace Fuck.Core.Config
{
    /// <summary>
    /// ���л��뷴���л����ļ�
    /// </summary>
    public class SerializationHelper
    {
        private static object lockObj = new object();
        #region JSON
        /// <summary>
        /// ���������л�������
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="obj"></param>
        public static void SerializableToJson(string fileName, object obj)
        {
            lock (lockObj)
            {
                using (FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate))
                {
                    using (StreamWriter sw = new StreamWriter(fs, Encoding.UTF8))
                    {
                        sw.Write(Newtonsoft.Json.JsonConvert.SerializeObject(obj));
                    }
                }
            }
        }
        /// <summary>
        /// �����Ʒ����л��Ӵ��̵��ڴ����
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static T DeserializeFromJson<T>(string fileName)
        {
            try
            {
                using (FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate))
                {

                    using (StreamReader sw = new StreamReader(fs, Encoding.UTF8))
                    {
                        return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(sw.ReadToEnd());
                    }

                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion
    }

}
