using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UNI_Management.Common
{
    public interface ICacheManager
    {
        T Get<T>(string key, ref bool hasValue);
        void Set(string key, object? data, int cacheTime);

        bool IsSet(string key);

        void RemoveByPattern(string pattern);

        void Remove(string key);
    }
}
