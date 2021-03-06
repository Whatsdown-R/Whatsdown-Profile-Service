using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;

namespace Whatsdown_ProfileService.caching
{
    public class MemoryCache : IMemoryCache
    {

        public IDistributedCache _cache;
        public BinaryFormatter binFormatter;

        public MemoryCache(IDistributedCache _cache)
        {
            binFormatter = new BinaryFormatter();
            this._cache = _cache;
        }

        public T getCache<T>(string key) where T : class
        {
            try
            {
                byte[] values = _cache.Get(key);

                if (values == null) return null;

                BinaryFormatter binaryFormatter = new BinaryFormatter();

                using(MemoryStream memoryStream = new MemoryStream(values))
                {
                    return binaryFormatter.Deserialize(memoryStream) as T;
                }
            }catch (Exception ex)
            {
                throw ex;
            }
        }


        public void setCache<T>(T values, string key)
        {
            try
            {
                MemoryStream mStream = new MemoryStream();

                binFormatter.Serialize(mStream, values);

                mStream.ToArray();

                DistributedCacheEntryOptions cacheEntryOptions = new DistributedCacheEntryOptions()
                {
                    AbsoluteExpiration = DateTime.Now.AddMinutes(30),
                    SlidingExpiration = TimeSpan.FromSeconds(30)
                };

                _cache.Set(key, mStream.ToArray());

                mStream.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void removeCache(string key)
        {
            try
            {
                _cache.Remove(key);
            }catch(Exception ex)
            {
                throw ex;
            }
        }

     
    }
}
