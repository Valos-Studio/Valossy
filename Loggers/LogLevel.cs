using Godot;

namespace Valossy.Loggers
{
    public partial class LogLevel : Node
    {
        public static readonly LogLevel Info = new LogLevel(LogLevelEnum.INFO, 0);

        public static readonly LogLevel Debug = new LogLevel(LogLevelEnum.DEBUG, 1);

        public static readonly LogLevel Trace = new LogLevel(LogLevelEnum.TRACE, 2);

        public LogLevel (){}
        
        public LogLevel (LogLevelEnum logLevelEnum, int logLevel)
        {
            this.LogLevelEnum = logLevelEnum;
            this.LogLevelValue = logLevel;
        }

        public LogLevelEnum LogLevelEnum { get; private set; }

        public int LogLevelValue { get; private set; }
    }

    public enum LogLevelEnum
    {
        INFO,
        DEBUG,
        TRACE
    }
}