using EasyCaching.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace AdminSystem.Application.Services
{
    public class CachingService: ICachingService
    {
        public IEasyCachingProvider CacheProvider { get; }
        public CachingService(IEasyCachingProviderFactory factory)
        {
            this.CacheProvider = factory.GetCachingProvider("m1");
        }

        
    }
}
