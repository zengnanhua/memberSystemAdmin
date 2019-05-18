using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Linq;

namespace AdminSystem.Application.Queries
{
    public class SysmanageQuery: ISysmanageQuery
    {
        private string _connectionString = string.Empty;
        public SysmanageQuery(string constr)
        {
            // Microsoft.AspNetCore.SignalR.Hub
            _connectionString = !string.IsNullOrWhiteSpace(constr) ? constr : throw new ArgumentNullException(nameof(constr));
        }
        /// <summary>
        /// 获取所有属性
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public async Task<PageView<GetAttributeListResult>> GetAttributeList(GetAttributeListParameter param)
        {
            
            string sql = @"SELECT * FROM Zmn_Sys_Attributes a inner join Zmn_Sys_Attribute_Details b
                            on a.AttrCode=b.AttrCode 
                            where a.AttrCode in (
                              select d.AttrCode from (select c.AttrCode FROM Zmn_Sys_Attributes c ORDER BY c.CreateTime desc   LIMIT @page,@pageSize ) d
                            )
                             ";
            string sqlTotal = @"select count(1) total from Zmn_Sys_Attributes ";


            using (var connection = new MySqlConnection(_connectionString))
            {
                var list = (await connection.QueryAsync<GetAttributeListDto>(sql, new {page=(param.Page-1)*param.PageSize, pageSize= param.Page * param.PageSize })).ToList();
                var total = connection.Query<int>("select count(1) total from Zmn_Sys_Attributes").FirstOrDefault();
                List<GetAttributeListResult> result = new List<GetAttributeListResult>();
                foreach (var temp in list)
                {
                    var obj= result.FirstOrDefault(c => c.AttrCode == temp.AttrCode);
                    if (obj == null)
                    {
                        obj= new GetAttributeListResult();
                        obj.AttrCode = temp.AttrCode;
                        obj.AttrDescr = temp.AttrDescr;
                        obj.DetailList = new List<GetAttributeListDetail>();
                        result.Add(obj);
                    }
                    obj.DetailList.Add(new GetAttributeListDetail() {AttrText=temp.AttrText,AttrValue=temp.AttrValue });

                }

                PageView<GetAttributeListResult> pageView = new PageView<GetAttributeListResult>();
                pageView.Data = result;
                pageView.Total = total;
                return pageView;
            }

             
        }
    }
}
