using StackExchange.Redis;
using System;

namespace Fuck.Core.Redis
{
    /// <summary>
    /// 实现redis的进程分布锁
    /// </summary>
    public class RedisLock
    {
        /// <summary>
        /// 分布式锁
        /// </summary>
        /// <param name="key">锁key</param>
        /// <param name="lockExpirySeconds">锁自动超时时间(秒)</param>
        /// <param name="waitLockMs">等待锁时间(秒)</param>
        /// <returns></returns>
        public static void Lock(string key, string value, Action action, int lockMilliseconds = 1000)
        {
            IDatabase client = RedisManager.Instance.GetDatabase();
            if (client.LockTake(key, value, TimeSpan.FromMilliseconds(lockMilliseconds)))
            {
                try
                {
                    action();
                }
                catch (Exception)
                {

                    throw;
                }
                finally
                {
                    client.LockRelease(key, value);
                }
            }

        }


    }
}
