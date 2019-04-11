using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using AdminSystem.Application.Queries;

namespace UserIdentity.Controllers
{
    [Route("api/[controller]")]
    //[Authorize]
    public class ValuesController : Controller
    {
        private IApplicationUserQuery _apptionUserQuery;
        public ValuesController(IApplicationUserQuery apptionUserQuery)
        {
            _apptionUserQuery = apptionUserQuery;
        }
        /// <summary>
        /// 获取用户 测试
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IEnumerable<object>> Get()
        {
            var list=await _apptionUserQuery.GetUserAsync(1);
            return new object[] { "value1", list };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
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
