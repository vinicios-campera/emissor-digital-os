using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace OrderService.Extensions
{
    public static class ExceptionExtensions
    {
        public static string Details(this Exception exception)
        {
            var builder = new StringBuilder();

            while (exception != null)
            {
                if (!string.IsNullOrEmpty(exception.StackTrace))
                {
                    string[] lines = exception.StackTrace.Split('\r', '\n');

                    for (int i = 0; i < lines.Length; i++)
                        builder.AppendLine(string.Format("\t\t{0}", lines[i].Trim()));
                }

                builder.AppendLine(string.Format("\t{0}", exception.Message));
                exception = exception.InnerException!;
                builder.AppendLine();
            }

            string details = builder.ToString();
            return details;
        }

        public static Exception Failin(this Exception exception, string message = "", [CallerMemberName] string sourceMemberName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNo = 0)
        {
            string errorMessage = string.Format("Fail in ({0}). File: {1}. Line: {2}.", sourceMemberName, sourceFilePath, sourceLineNo);

            var ex = !string.IsNullOrEmpty(message)
                ? new Exception(errorMessage, new Exception(message, exception))
                : new Exception(errorMessage, exception);

            return ex;
        }

        public static Exception FailinTail(this Exception exception, string message = "", [CallerMemberName] string sourceMemberName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNo = 0)
        {
            string errorMessage = string.Format("Fail in ({0}). File: {1}. Line: {2}.", sourceMemberName, sourceFilePath, sourceLineNo);

            var ex = !string.IsNullOrEmpty(message)
                ? new Exception(errorMessage, new Exception(message, exception))
                : new Exception(errorMessage, exception);

            return new Exception(exception.InnerMost(), ex);
        }

        public static bool From(this Exception exception, string source)
        {
            if (string.IsNullOrEmpty(source))
                return false;

            Exception ex = exception;

            if (ex.Source == source)
                return true;

            while (ex.InnerException != null)
            {
                ex = ex.InnerException;

                if (ex.Source == source)
                    return true;
            }

            return false;
        }

        public static bool From(this Exception exception, Type typeSource)
        {
            string? source;
            try
            {
                source = typeSource.Assembly.GetName().Name;
            }
            catch
            {
                source = null;
            }
            return exception.From(source!);
        }

        public static bool Has<T>(this Exception exception) where T : Exception
        {
            var ex = exception.OfType<T>();
            return ex != null;
        }

        public static string InnerMost(this Exception exception)
        {
            while (exception.InnerException != null) exception = exception.InnerException;
            return exception.Message;
        }

        public static string InnerMostDetails(this Exception exception)
            => string.Format("{0} \r\n{1}", exception.InnerMost(), exception.Details());

        public static Exception InnerMostException(this Exception exception)
        {
            while (exception.InnerException != null) exception = exception.InnerException;
            return exception;
        }

        public static bool IsInnerMostOfType<T>(this Exception exception)
        {
            while (exception.InnerException != null) exception = exception.InnerException;
            return typeof(T) == exception.GetType();
        }

        public static T? OfType<T>(this Exception exception) where T : Exception
        {
            Exception ex = exception;

            if (ex.GetType() == typeof(T))
                return (T)ex;

            while (ex.InnerException != null)
            {
                ex = ex.InnerException;

                if (ex.GetType() == typeof(T))
                    return (T)ex;
            }

            return null;
        }
    }
}