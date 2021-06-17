using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Whatsdown_ProfileService.caching
{
    public interface IMemoryCache
    {

        public T getCache<T>(string key) where T : class; 

        public void setCache<T>(T values, string key);

        public void removeCache(string key);
    }
}
