using System;
using System.IO;
using System.Runtime.CompilerServices;
using Godot;

namespace Valossy.Loggers;

public static class Logger
{
    public static readonly LogLevel CurrentLogLevel = LogLevel.Trace;

    public static void Info(object message, [CallerMemberName] string memberName = "", [CallerFilePath]string callerFilePath = null)
    {
        if(message != null)
        {
            Info(message.ToString(), memberName, callerFilePath);
        }
    }

    public static void Debug(object message, [CallerMemberName] string memberName = "", [CallerFilePath]string callerFilePath = null)
    {
        if(message != null)
        {
            Debug(message.ToString(), memberName, callerFilePath);
        }
    }

    public static void Trace(object message, [CallerMemberName] string memberName = "", [CallerFilePath]string callerFilePath = null)
    {
        if(message != null)
        {
            Trace(message.ToString(), memberName, callerFilePath);
        }
    }

    public static void Info(string message, [CallerMemberName] string memberName = "", [CallerFilePath]string callerFilePath = null)
    {
        Print(LogLevel.Info, message, memberName, callerFilePath);
    }
    
    public static void Debug(string message, [CallerMemberName] string memberName = "", [CallerFilePath]string callerFilePath = null)
    {
        Print(LogLevel.Debug, message, memberName, callerFilePath);
    }
    
    public static void Trace(string message, [CallerMemberName] string memberName = "", [CallerFilePath]string callerFilePath = null)
    {
        Print(LogLevel.Trace, message, memberName, callerFilePath);
    }

    public static void Print(LogLevel logLevel, string message, string memberName, string callerFilePath)
    {
        if(CurrentLogLevel.LogLevelValue < logLevel.LogLevelValue)
        {
            return;
        }

        string callerTypeName = Path.GetFileNameWithoutExtension(callerFilePath);
        
        GD.Print($"{logLevel.LogLevelEnum} - {callerTypeName}.{memberName}: {message}");
    }

    public static void Error(object message, [CallerMemberName] string memberName = "", [CallerFilePath]string callerFilePath = null)
    {
        string callerTypeName = Path.GetFileNameWithoutExtension(callerFilePath);
        
        GD.PrintErr($"{callerTypeName}.{memberName}: {message}");
    }
    
    public static void Error(string message, Exception exception, [CallerMemberName] string memberName = "", [CallerFilePath]string callerFilePath = null)
    {
        string callerTypeName = Path.GetFileNameWithoutExtension(callerFilePath);
        
        GD.PrintErr($"{callerTypeName}.{memberName}: {message}", exception);
    }
}