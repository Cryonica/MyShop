using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;

namespace MyShop.Helpers
{
    public static class SessionHelper
    {
        public static void SetObjectAsJson(this ISession session, string key, object value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }

        public static T GetObjectFromJson<T>(this ISession session, string key)
        {
            var value = session.GetString(key);
            return value == null ? default(T) : JsonConvert.DeserializeObject<T>(value);
        }
        public static void SetObjectAsBinary(this ISession session, string key, object value)
        {
            if (value !=null)
            {
                BinaryFormatter bf = new BinaryFormatter();
                using (MemoryStream ms = new MemoryStream())
                {
                    bf.Serialize(ms, value);
                    session.SetObjectAsBinary(key, ms.ToArray());
                }
            }
               
        }
        public static T GetObjectFromBinary<T>(this ISession session, string key) where T:class
        {
            byte[] bytes;
            session.TryGetValue(key, out bytes);
            return bytes as T;
        }
    }
}
