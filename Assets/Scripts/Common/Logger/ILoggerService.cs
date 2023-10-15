using System;

namespace Highborne.Common.Logger
{
    public interface ILoggerService
    {
        void Log(string message);
        void LogError(string errorMessage);
        void LogException(Exception exception);
    }
}