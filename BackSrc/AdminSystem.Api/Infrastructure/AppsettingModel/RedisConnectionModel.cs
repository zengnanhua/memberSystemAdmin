using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminSystem.Api.Infrastructure
{
    /// <summary>
    /// redis链接配置
    /// </summary>
    public class RedisConnectionModel
    {
        public string Host { get; set; }
        public int Port { get; set; }
    }
}
