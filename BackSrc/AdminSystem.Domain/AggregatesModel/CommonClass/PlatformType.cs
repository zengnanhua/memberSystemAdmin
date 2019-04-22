using AdminSystem.Domain.AggregatesModel.CommonClass;
using AdminSystem.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace AdminSystem.Domain.AggregatesModel.Common
{
    public enum PlatformType
    {
        [Remark("Pc")]
        Pc =1,
        [Remark("App")]
        App =2
    }
}
