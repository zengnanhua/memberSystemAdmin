using System;
using System.Collections.Generic;
using System.Text;

namespace AdminSystem.Application.Queries
{
    public class GetAttributeListParameter
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
    }
    public class GetAttributeListDto
    {
        public string AttrCode { get; set; }
        public string AttrDescr { get; set; }
        public string AttrValue { get; private set; }
        public string AttrText { get; private set; }

    }
    public class GetAttributeListResult
    {
        public string AttrCode { get;  set; }
        public string AttrDescr { get;  set; }

        public List<GetAttributeListDetail> DetailList { get;  set; }
    }
    public class GetAttributeListDetail
    {
        public string AttrValue { get;  set; }
        public string AttrText { get;  set; }
    }
}
