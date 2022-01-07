using Fuck.Core.Config;
using Fuck.Core.Http;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Web.Http;

namespace WebApplication1.Controllers
{
    public class ValuesController : ApiController
    {
        // GET api/values
        public IEnumerable<KeyValue> Get()
        {
            JsonConfigFile jsonConfigFile = new JsonConfigFile("test");
            KeyValue result = jsonConfigFile.Get("name");
            return new KeyValue[] { result };
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public string Post([FromBody] string value)
        {
            Thread.Sleep(30000);//耗时1分钟
            return "成功响应：" + DateTime.Now;
        }

        // PUT api/values/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
