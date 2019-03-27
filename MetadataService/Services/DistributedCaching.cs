using Microsoft.Extensions.Caching.Distributed;
using System.Threading;
using System;
using System.Threading.Tasks;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using Newtonsoft.Json;

namespace MetadataService
{
    public static class DistributedCaching
    {
        public async static Task SetAsync<T>(this IDistributedCache distributedCache, string key, T value, DistributedCacheEntryOptions options, CancellationToken token = default(CancellationToken))
        {
            var jsonData = JsonConvert.SerializeObject(value);
            await distributedCache.SetStringAsync(key, jsonData, options, token);
        }

        public async static Task<T> GetAsync<T>(this IDistributedCache distributedCache, string key, CancellationToken token = default(CancellationToken)) where T : class
        {

            var result = await distributedCache.GetStringAsync(key, token);
            if (result != null)
                return JsonConvert.DeserializeObject<T>(result);
            else
                return null;
        }
    }
    public static class Serialization
    {
        public static byte[] ToByteArray(this object obj)
        {
            if (obj == null)
            {
                return null;
            }
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            using (MemoryStream memoryStream = new MemoryStream())
            {
                binaryFormatter.Serialize(memoryStream, obj);
                return memoryStream.ToArray();
            }
        }

        public static T FromByteArray<T>(this byte[] byteArray) where T : class
        {
            if (byteArray == null)
            {
                return default(T);
            }
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            using (MemoryStream memoryStream = new MemoryStream(byteArray))
            {
                return binaryFormatter.Deserialize(memoryStream) as T;
            }
        }

    }
}
