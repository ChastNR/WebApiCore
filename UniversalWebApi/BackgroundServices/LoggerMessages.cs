using System;
using Microsoft.Extensions.Logging;

namespace UniversalWebApi.BackgroundServices
{
    public static class LoggerExtension
    {
        public static string ServiceStarted<T>(this ILogger logger) => $"{typeof(T).Name} started at {DateTime.Now}";

        public static string ServiceStopped<T>(this ILogger logger) => $"{typeof(T).Name} stopped at {DateTime.Now}";

        public static string ServiceError<T>(this ILogger logger, Exception e) => $"{typeof(T).Name} stopped working due to: \n {e.Message}";
    }
}