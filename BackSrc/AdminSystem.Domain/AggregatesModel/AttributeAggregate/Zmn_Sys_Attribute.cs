using AdminSystem.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace AdminSystem.Domain.AggregatesModel.AttributeAggregate
{
    public class Zmn_Sys_Attribute : ValueObject, IAggregateRoot
    {
        public string AttrCode { get; private set; }
        public string AttrDescr { get; private set; }
        public DateTime CreateTime { get; private set; }
        public List<Zmn_Sys_Attribute_Detail> DetailList { get; private set; }

        protected Zmn_Sys_Attribute()
        {
            this.DetailList = new List<Zmn_Sys_Attribute_Detail>();
            this.CreateTime = DateTime.Now;
        }

        public Zmn_Sys_Attribute(string attrCode, string attrDescr) : this()
        {
            this.AttrCode = attrCode;
            this.AttrDescr = attrDescr;
            this.CreateTime = DateTime.Now;
        }
        public void AddAttributeDetail(string attrValue,string attrText)
        {
            DetailList.Add(new Zmn_Sys_Attribute_Detail(this.AttrCode,attrValue, attrText));
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return AttrCode;
            yield return AttrDescr;
        }
    }
}
