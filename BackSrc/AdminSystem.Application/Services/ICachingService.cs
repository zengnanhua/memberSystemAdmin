using EasyCaching.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace AdminSystem.Application.Services
{
    public interface ICachingService
    {
        IEasyCachingProvider CacheProvider { get; }
    }
}
