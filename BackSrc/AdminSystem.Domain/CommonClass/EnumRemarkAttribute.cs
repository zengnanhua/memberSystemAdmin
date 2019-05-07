using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace AdminSystem.Domain.CommonClass
{
    public class EnumRemarkAttribute : Attribute
    {
        public EnumRemarkAttribute(string remark)
        {
            Remark = remark;
        }

        public string Remark { get;private set; }

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
            object[] attrs = fd.GetCustomAttributes(typeof(EnumRemarkAttribute), false);
            string name = string.Empty;
            foreach (EnumRemarkAttribute attr in attrs)
            {
                name = attr.Remark;
            }
            return name;
        }
    }
}
