using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using AdminSystem.Application.Queries;
using AdminSystem.Application.Commands;

namespace UserIdentity.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [Authorize]
    public class ValuesController : Controller
    {
       
        public ValuesController()
        {
           
        }
        /// <summary>
        /// 获取用户 测试
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IEnumerable<object>> Get()
        {
           
            //var list=await _apptionUserQuery.GetUserAsync(1);
            return new object[] { "value1", "sdfa" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        /// <summary>
        /// 获取用户
        /// </summary>
        /// <param name="param"></param>
        [HttpPost]
        [Authorize]
        public void Post(ddd param)
        {
        }
        public class ddd
        {
            /// <summary>
            /// 用户名
            /// </summary>
            public string Name { get; set; }
        }
        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
