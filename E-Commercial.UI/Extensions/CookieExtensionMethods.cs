using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Commercial.UI.Extensions
{
    public static class CookieExtensionMethods
    {
        public static void SetObject(this IResponseCookies cookie, string key, object value)
        {
            string stringOfObject = JsonConvert.SerializeObject(value);
            cookie.Append(key, stringOfObject);
        }

        public static T GetObject<T>(this IRequestCookieCollection cookie, string key) where T : class
        {
            string objectOfString = cookie[key];
            if (string.IsNullOrEmpty(objectOfString))
            {
                return null;
            }

            T value = JsonConvert.DeserializeObject<T>(objectOfString);
            return value;
        }
    }
}
