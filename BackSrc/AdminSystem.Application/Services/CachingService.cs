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
        /// <summary>
        /// 代码是否可以执行
        /// </summary>
        public bool IsAcquired { get; set; } = true;
        public LockDistribute(Action func)
        {
            this._func = func;
        }
        ~LockDistribute()
        {
            _func();
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
        /// 阻塞锁 分布式锁
        /// </summary>
        /// <returns></returns>
        public LockDistribute AcquireBlock(string key)
        {
            var token = Guid.NewGuid().ToString();// Environment.MachineName;
            var redisDatabase = _redisDatabaseProvider.GetDatabase();
            var b = false;
            var stopwatch = System.Diagnostics.Stopwatch.StartNew();
            
            while (!b)
            {
                b = redisDatabase.LockTake(key, token, TimeSpan.FromSeconds(20));
                if (!b)
                {
                    if (stopwatch.Elapsed >= TimeSpan.FromSeconds(9)) //如果阻塞超过9秒就退出
                    {
                        throw new Exception("阻塞分布式锁超时");
                        break;
                    }
                    System.Threading.Thread.Sleep(200);
                } 
            }
            
            LockDistribute lockDistribute = new LockDistribute(() =>
            {
                var vv = redisDatabase.LockRelease(key, token);
            });
            lockDistribute.IsAcquired = b;
            return lockDistribute;
        }
        /// <summary>
        ///redis 分布式锁
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public LockDistribute AcquireLock(string key)
        {
            var token = Guid.NewGuid().ToString();// Environment.MachineName;
            var redisDatabase = _redisDatabaseProvider.GetDatabase();
            
            var b = redisDatabase.LockTake(key, token, TimeSpan.FromSeconds(20));
            LockDistribute lockDistribute = new LockDistribute(() =>
            {
                var vv = redisDatabase.LockRelease(key, token);
            });
            lockDistribute.IsAcquired = b;
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
