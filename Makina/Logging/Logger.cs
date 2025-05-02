using NLog;

namespace Makina.Logging
{
    public static class Logger
    {
        private static readonly NLog.Logger NLogger = LogManager.GetCurrentClassLogger();

        // NLog configuration is handled by nlog.config in the startup project (Sandbox)

        public static void Debug(string message)
        {
            NLogger.Debug(message);
        }

        public static void Info(string message)
        {
            NLogger.Info(message);
        }

        public static void Warning(string message)
        {
            NLogger.Warn(message);
        }

        public static void Error(string message)
        {
            NLogger.Error(message);
        }

        public static void Fatal(string message)
        {
            NLogger.Fatal(message);
        }
    }
} 