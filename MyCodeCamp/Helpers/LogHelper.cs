using Microsoft.Extensions.Logging;

namespace MyCodeCamp.Helpers
{
    public static  class LogHelper
    {
        private static ILoggerFactory _loggerFactory;

        public static void Init(string fileName)
        {
            _loggerFactory = new LoggerFactory();
            _loggerFactory.AddFile(fileName);
        }

        public static ILogger<T> CreateLogger<T>() =>
               _loggerFactory.CreateLogger<T>();
       
    }
}