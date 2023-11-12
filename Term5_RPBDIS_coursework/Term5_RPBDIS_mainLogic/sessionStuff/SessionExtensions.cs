﻿using Microsoft.AspNetCore.Http;
using System.Text;
using Newtonsoft.Json;

namespace Term5_RPBDIS_mainLogic.sessionStuff {
    public static class SessionExtensions {
        public static void Set<T>(this ISession session, string key, T value) {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }

        public static T Get<T>(this ISession session, string key) {
            var value = session.GetString(key);

            return value == null ? default : JsonConvert.DeserializeObject<T>(value);
        }
    }
}