using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace AdminSystem.Infrastructure.Common
{
    public class ResultCode
    {
        /// <summary>
        /// 返回数据枚举
        /// </summary>
        public enum ResultCodeType
        {
            /// <summary>
            /// 正常
            /// </summary>

            SuccessCode = 10000,

            /// <summary>
            /// 失败
            /// </summary>

            FailCode = 10007,

        }
    }
    public class ResultData<T>
    {
        public string resultCode { get; set; }
        public string resultMessage { get; set; }
        public T data { get; set; }
        public object extend { get; set; }
        /// <summary>
        /// 默认构造函数，返回结果为Success
        /// Since:2017-09-15
        /// </summary>
        public ResultData()
        {
            resultCode = ((int)ResultCode.ResultCodeType.SuccessCode).ToString();
            resultMessage = "成功";
            data = default(T);
        }



        /// <summary>
        /// 曾南华20171012
        /// 静态方法，创建ResultData对象
        /// </summary>
        /// <param name="code">错误编码</param>
        /// <param name="message">错误描述</param>
        /// <param name="datas">数据</param>
        /// <returns></returns>
        public static ResultData<T> CreateResultData<T>(ResultCode.ResultCodeType code, string message = "", T datas = default(T), object _extend = null)
        {
            string codeStr = ((int)code).ToString();

            return new ResultData<T>()
            {
                resultCode = codeStr,
                resultMessage = message,
                data = datas,
                extend = _extend,
            };
        }
        /// <summary>
        /// 曾南华20171012
        /// 静态方法，创建成功ResultData对象
        /// </summary>
        /// <param name="code">错误编码</param>
        /// <param name="message">错误描述</param>
        /// <param name="datas">数据</param>
        /// <returns></returns>
        public static ResultData<T> CreateResultDataSuccess(string message = "", T datas = default(T), object _extend = null)
        {
            string codeStr = ((int)ResultCode.ResultCodeType.SuccessCode).ToString();

            return new ResultData<T>()
            {
                resultCode = codeStr,
                resultMessage = message,
                data = datas,
                extend = _extend,
            };
        }
        /// <summary>
        /// 曾南华20171012
        /// 静态方法，创建成功ResultData对象
        /// </summary>
        /// <param name="code">错误编码</param>
        /// <param name="message">错误描述</param>
        /// <param name="datas">数据</param>
        /// <returns></returns>
        public static ResultData<T> CreateResultDataFail(string message = "", T datas = default(T), object _extend = null)
        {
            string codeStr = ((int)ResultCode.ResultCodeType.FailCode).ToString();

            return new ResultData<T>()
            {
                resultCode = codeStr,
                resultMessage = message,
                data = datas,
                extend = _extend,
            };
        }
        /// <summary>
        /// 曾南华20171017
        /// 判断返回是否成功
        /// </summary>
        /// <returns></returns>
        public bool IsSuccess()
        {
            return resultCode == ((int)ResultCode.ResultCodeType.SuccessCode).ToString();
        }

        /// <summary>
        /// 曾南华 20171013
        /// 获取data里面数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T GetDataValue<T>()
        {
            if (data != null)
            {
                return JsonConvert.DeserializeObject<T>(JsonConvert.SerializeObject(data));
            }

            return default(T); //对引用类型返回为空 ，对值类型类型返回0
        }
    }
}
