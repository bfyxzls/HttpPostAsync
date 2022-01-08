using Fuck.Core.Config;
using Fuck.Core.Http;
using Fuck.Core.User;
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

            JsonConfigFile hellConfig = new JsonConfigFile("hello");
            KeyValue result1 = hellConfig.Get("title");

            return new KeyValue[] { result,result1 };
        }
        IUserBlanceService userBlanceService;
        public ValuesController()
        {
            userBlanceService = new UserBlanceService();
            userBlanceService.OnIncream += UserBlanceService_OnIncream;
        }

        private void UserBlanceService_OnIncream(int arg1, decimal arg2)
        {
            Console.WriteLine("OnIncream会执行");
        }

        // GET api/values/5
        public string Get(int id)
        {
            userBlanceService.incream(1, 10m);
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
