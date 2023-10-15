using System;
using UnityEngine;

namespace Highborne.Common.Logger
{
    public sealed class UnityLoggerService : ILoggerService
    {
        public void Log(string message)
        {
            Debug.Log(message);
        }

        public void LogError(string errorMessage)
        {
            Debug.LogError(errorMessage);
        }

        public void LogException(Exception exception)
        {
            Debug.LogException(exception);
        }
        
    }
}