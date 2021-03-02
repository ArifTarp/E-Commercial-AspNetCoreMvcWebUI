using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace E_Commercial.UI.Extensions
{
    public static class SessionExtensionMethods
    {
        public static void SetObject(this ISession session, string key, object value)
        {
            string stringOfObject = JsonConvert.SerializeObject(value);
            session.SetString(key,stringOfObject);
        }

        public static T GetObject<T>(this ISession session, string key) where T:class
        {
            string objectOfString = session.GetString(key);
            if (string.IsNullOrEmpty(objectOfString))
            {
                return null;
            }

            T value = JsonConvert.DeserializeObject<T>(objectOfString);
            return value;
        }
    }
}
