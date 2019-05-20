using EasyCaching.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace AdminSystem.Application.Services
{
    public interface ICachingService
    {
        /// <summary>
        /// 内存缓存
        /// </summary>
        IEasyCachingProvider InMemoryCacheProvider { get; }
        /// <summary>
        /// redis缓存
        /// </summary>
        IEasyCachingProvider RedisCacheProvider { get; }
        /// <summary>
        /// 内存缓存 作为一级缓存 redis作为二级缓存
        /// </summary>
        IHybridCachingProvider HybridCachingProvider { get; }
        /// <summary>
        ///redis 分布式锁
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        IDisposable AcquireLock(string key);
    }
}
