using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AdminSystem.Application.Queries
{
    public interface ISysmanageQuery
    {
        /// <summary>
        /// 获取所有属性
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        Task<PageView<GetAttributeListResult>> GetAttributeList(GetAttributeListParameter param);
    }
}
