using AdminSystem.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace AdminSystem.Domain.AggregatesModel.AttributeAggregate
{
    public class Zmn_Sys_Attribute_Detail : ValueObject
    {
        public int Id { get; private set; }
        public string AttrValue { get; private set; }
        public string AttrText { get; private set; }
        public string AttrCode { get; private set; }
        protected Zmn_Sys_Attribute_Detail()
        {

        }
        public Zmn_Sys_Attribute_Detail(string attrCode,string attrValue,string attrText)
        {
            this.AttrCode = attrCode;
            this.AttrValue = attrValue;
            this.AttrText = attrText;
        }
        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return AttrValue;
            yield return AttrText;
        }
    }
}
