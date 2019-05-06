using AdminSystem.Domain.SeedWork;
using System;
using System.Collections.Generic;

namespace AdminSystem.Domain.AggregatesModel.UserAggregate
{
    /// <summary>
    /// 用户地址
    /// </summary>
    public class Address : ValueObject
    {
        /// <summary>
        /// 街道
        /// </summary>
        public String Street { get; private set; }
        /// <summary>
        /// 市
        /// </summary>
        public String City { get; private set; }
        /// <summary>
        /// 省
        /// </summary>
        public String Province { get; private set; }
        /// <summary>
        /// 区县
        /// </summary>
        public String Country { get; private set; }
        /// <summary>
        /// 完整地址
        /// </summary>
        public String FullAddress { get; private set; }

        private Address() { }

        public Address(string province, string city, string country, string street, string fullAddress)
        {
            Province = province;
            City = city;
            Country = country;
            Street = street;
            FullAddress = fullAddress;
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            // Using a yield return statement to return each element one at a time
            yield return Street;
            yield return City;
            yield return Province;
            yield return Country;
            yield return FullAddress;
        }
    }
}
