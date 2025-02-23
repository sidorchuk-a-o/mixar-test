using System;

namespace AD.ToolsCollection
{
    public delegate void FailAction(long errorCode = -1, string message = null);

    public static class ActionExtensions
    {
        public static void SafeInvoke(this Action action)
        {
            action?.Invoke();
        }

        public static void SafeInvoke<T1>(this Action<T1> action, T1 t1)
        {
            action?.Invoke(t1);
        }

        public static void SafeInvoke<T1, T2>(this Action<T1, T2> action, T1 t1, T2 t2)
        {
            action?.Invoke(t1, t2);
        }

        public static void SafeInvoke(this FailAction action)
        {
            action?.Invoke();
        }

        public static void SafeInvoke(this FailAction action, long errorCode, string message)
        {
            action?.Invoke(errorCode, message);
        }

        public static TResult SafeInvoke<TResult>(this Func<TResult> action)
        {
            return action is not null ? action.Invoke() : default;
        }

        public static TResult SafeInvoke<T1, TResult>(this Func<T1, TResult> action, T1 t1)
        {
            return action is not null ? action.Invoke(t1) : default;
        }

        public static TResult SafeInvoke<T1, T2, TResult>(this Func<T1, T2, TResult> action, T1 t1, T2 t2)
        {
            return action is not null ? action.Invoke(t1, t2) : default;
        }
    }
}