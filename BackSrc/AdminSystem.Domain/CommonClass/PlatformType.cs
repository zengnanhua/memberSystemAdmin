using AdminSystem.Domain.CommonClass;
using AdminSystem.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace AdminSystem.Domain.CommonClass
{
    [EnumRemark("客户端类别")]
    public enum PlatformType
    {
        [EnumRemark("Pc")]
        Pc =1,
        [EnumRemark("App")]
        App =2
    }
}