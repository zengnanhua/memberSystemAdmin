using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace AdminSystem.Domain.CommonClass
{
    public class RemarkAttribute : Attribute
    {
        public RemarkAttribute(string remark)
        {
            Remark = remark;
        }

        public string Remark { get; set; }

        /// <summary>
        /// 获取枚举备注
        /// </summary>
        /// <param name="_enum"></param>
        /// <returns></returns>
        public static string GetEnumRemark(Enum _enum)
        {

            Type type = _enum.GetType();
            FieldInfo fd = type.GetField(_enum.ToString());
            if (fd == null) return string.Empty;
            object[] attrs = fd.GetCustomAttributes(typeof(RemarkAttribute), false);
            string name = string.Empty;
            foreach (RemarkAttribute attr in attrs)
            {
                name = attr.Remark;
            }
            return name;
        }
    }
}
