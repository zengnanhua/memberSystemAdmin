using EasyCaching.Core;
using EasyCaching.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdminSystem.Application.Services
{
    public class LockDistribute : IDisposable
    {
        private Action _func;
        public LockDistribute(Action func)
        {
            this._func = func;
        }
        public void Dispose()
        {
            _func();
        }
    }


    public class CachingService : ICachingService
    {
        public IHybridCachingProvider HybridCachingProvider { get; }
        public IEasyCachingProvider InMemoryCacheProvider { get; }
        public IEasyCachingProvider RedisCacheProvider { get; }

        public IRedisDatabaseProvider _redisDatabaseProvider { get; }
        public CachingService(IEasyCachingProviderFactory factory, IHybridCachingProvider provider, IEnumerable<IRedisDatabaseProvider> dbProviders)
        {

            this.HybridCachingProvider = provider;
            this.InMemoryCacheProvider = factory.GetCachingProvider("m1");
            this.RedisCacheProvider = factory.GetCachingProvider("redis1");// factory.GetRedisProvider("redis1");
            
            this._redisDatabaseProvider = dbProviders.Single(x => x.DBProviderName.Equals("redis1"));
         
        }
        /// <summary>
        ///redis 分布式锁
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public IDisposable AcquireLock(string key)
        {
            var token = "ccccc";// Guid.NewGuid().ToString();// Environment.MachineName;
            var redisDatabase = _redisDatabaseProvider.GetDatabase();
            var jj = redisDatabase.StringGet("dsd");
            var bb = redisDatabase.LockTake(key, token, TimeSpan.FromSeconds(20));
            LockDistribute lockDistribute = new LockDistribute(() =>
            {
                var vv = redisDatabase.LockRelease(key, token);
            });
            return lockDistribute;


            //单部署的时候进程锁
            //var token = false;
            //System.Threading.Monitor.Enter(key,ref token);
            //try
            //{
            //    LockDistribute lockDistribute = new LockDistribute(() =>
            //    {
            //        System.Threading.Monitor.Exit(key);
            //    });

            //    return lockDistribute;
            //}
            //finally
            //{
            //    if (token)
            //    {
            //        System.Threading.Monitor.Exit(key);
            //    }
            //}

        }

    }
}
