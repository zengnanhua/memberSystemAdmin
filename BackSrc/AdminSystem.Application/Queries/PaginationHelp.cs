using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;
using Dapper;
using System.Linq;
using System.Threading.Tasks;

namespace AdminSystem.Application.Queries
{
    public class PageView<T>
    {
        public List<T> Data { get; set; }
        public int Total { get; set; }
    }
    public class PaginationHelp
    {
        /// <summary>
        /// 获取分页总条数
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="connectionStr"></param>
        public static async Task<PageView<T>> GetPageDataAsync<T>(string sql,int page,int pageSize,DynamicParameters parameters,string _connectionStr)
        {
            PageView<T> pageView = new PageView<T>();
            using (var connection = new MySqlConnection(_connectionStr))
            {
                string selectSql = $"select * count from ({sql}) sssssssss ";
                pageView.Data= (await connection.QueryAsync<T>(sql, parameters)).ToList();


                parameters.Add("page", (page - 1)*page);
                parameters.Add("pageSize", page*pageSize);

                string totalSql = $"select count(1) count from ({sql}) sssssssss LIMIT @page,@pageSize";
                pageView.Total = await connection.QuerySingleAsync<int>(sql, parameters);

                return pageView;

            }
        }
        public static async Task<PageView<T>> GetPageDataAsync<T>(string sql, object model, string _connectionStr)
        {
            var parameters = ClassConvertDynamicParameters(model);
            if (parameters.ParameterNames == null || parameters.ParameterNames.ToList().Where(c => c.ToLower() == "page").Count() <= 0)
            {
                throw new Exception("分页参数没有 “page”");
            }
            if (parameters.ParameterNames == null || parameters.ParameterNames.ToList().Where(c => c.ToLower() == "pagesize").Count() <= 0)
            {
                throw new Exception("分页参数没有 “pagesize”");
            }

            PageView<T> pageView = new PageView<T>();
            using (var connection = new MySqlConnection(_connectionStr))
            {
                string selectSql = $"select * count from ({sql}) sssssssss ";
                pageView.Data = (await connection.QueryAsync<T>(sql, parameters)).ToList();

                string totalSql = $"select count(1) count from ({sql}) sssssssss LIMIT @page,@pageSize";
                pageView.Total = await connection.QuerySingleAsync<int>(sql, parameters);

                return pageView;

            }
        }
        /// <summary>
        /// 类型转换
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model"></param>
        /// <returns></returns>
        private static DynamicParameters ClassConvertDynamicParameters(object model)
        {
            DynamicParameters param = new DynamicParameters();
            var propertyInfoList= model.GetType().GetProperties();
            foreach (var propertyInfo in propertyInfoList)
            {
                param.Add(propertyInfo.Name.ToString(), propertyInfo.GetValue(model,null));
            }
            return param;
        }
        public static  PageView<T> GetPageData<T>(string sql, int page, int pageSize, DynamicParameters parameters, string _connectionStr)
        {
            PageView<T> pageView = new PageView<T>();
            using (var connection = new MySqlConnection(_connectionStr))
            {
                string selectSql = $"select * count from ({sql}) sssssssss ";
                pageView.Data = connection.Query<T>(sql, parameters).ToList();


                parameters.Add("page", (page - 1) * page);
                parameters.Add("pagesize", page * pageSize);

                string totalSql = $"select count(1) count from ({sql}) sssssssss LIMIT @page,@pageSize";
                pageView.Total =  connection.QuerySingle<int>(sql, parameters);

                return pageView;

            }
        }
        
    }
}
