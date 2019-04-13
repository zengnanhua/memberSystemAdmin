using System;
using System.Collections.Generic;
using System.Text;

namespace AdminSystem.Domain.Exceptions
{
    public class AdminSystemDomainException : Exception
    {
        public AdminSystemDomainException()
        { }

        public AdminSystemDomainException(string message)
            : base(message)
        { }

        public AdminSystemDomainException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
