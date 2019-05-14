using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserIdentity.Infrastructure.Queries
{
    public class UserDto
    {
        public int Id { get; set; }
        /// <summary>
        /// 用户姓名
        /// </summary>
        public string Name { get;  set; }
        /// <summary>
        ///  用户账号
        /// </summary>
        public string UserName { get;  set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string Pwd { get;  set; }
        /// <summary>
        /// 用户性别
        /// </summary>
        public string Sex { get;  set; }
        /// <summary>
        /// 用户手机号码
        /// </summary>
        public string Phone { get;  set; }
        /// <summary>
        /// 是否删除
        /// </summary>
        public bool IsDelete { get;  set; }
    }
}
