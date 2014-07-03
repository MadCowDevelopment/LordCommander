using System;

namespace LordCommander.Services
{
    public class ProgressDialogResult
    {
        public Exception Exception { get; private set; }

        public ProgressDialogResult(Exception exception)
        {
            Exception = exception;
        }

        public bool Success { get { return Exception == null; } }
    }
}