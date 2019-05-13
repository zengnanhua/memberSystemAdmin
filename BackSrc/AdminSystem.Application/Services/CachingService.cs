using EasyCaching.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace AdminSystem.Application.Services
{
    public class CachingService: ICachingService
    {
        public IHybridCachingProvider HybridCachingProvider { get; }
        public IEasyCachingProvider CacheProvider { get; }
        public IRedisCachingProvider RedisCacheProvider { get; }
        public CachingService(IEasyCachingProviderFactory factory, IHybridCachingProvider provider)
        {
            this.HybridCachingProvider = provider;
            this.CacheProvider = factory.GetCachingProvider("m1");
            this.RedisCacheProvider = factory.GetRedisProvider("redis1");
        }

        
    }
}
