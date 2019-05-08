using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using AdminSystem.Application.Queries;
using AdminSystem.Application.Commands;
using AdminSystem.Application.Services;

namespace UserIdentity.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    //[Authorize]
    public class ValuesController : Controller
    {
        IAccountQuery _accountQuery;
        ICachingService _cachingService;
        public ValuesController(IAccountQuery accountQuery, ICachingService cachingService)
        {
            this._accountQuery = accountQuery;
            this._cachingService = cachingService;
        }
        [Serializable]
        public class Person
        {
            public string Name { get; set; }
            public string Sex { get; set; }
            public string Age { get; set; }
        }
        /// <summary>
        /// 获取用户 测试
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<object> Get()
        {
            //var hello= _cachingService.CacheProvider.Get("dsd", () => "this is dome", TimeSpan.FromMinutes(1));
            //_cachingService.RedisCacheProvider.StringSet("dd", "this is dome");

           
           // var dd= _cachingService.RedisCacheProvider.StringGet("dd");
            //var list=await _accountQuery.GetPageMenuByUserId(1);
            return "sdfasd";
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
