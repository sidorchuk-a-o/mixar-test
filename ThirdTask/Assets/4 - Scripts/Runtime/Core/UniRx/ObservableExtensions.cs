using AD.ToolsCollection;
using System;
using UniRx;

namespace AD.ToolsCollection
{
    public static class ObservableExtensions
    {
        // == Subscribe ==

        public static IDisposable Subscribe<T>(
            this IObservable<T> source,
            Action onNext)
        {
            return source.Subscribe(_ => onNext.SafeInvoke());
        }

        public static IDisposable Subscribe<T>(
            this IObservable<T> source,
            Action onNext,
            Action<Exception> onError)
        {
            return source.Subscribe(_ => onNext.SafeInvoke(), onError);
        }

        public static IDisposable Subscribe<T>(
            this IObservable<T> source,
            Action onNext,
            Action onCompleted)
        {
            return source.Subscribe(_ => onNext.SafeInvoke(), onCompleted);
        }

        public static IDisposable Subscribe<T>(
            this IObservable<T> source,
            Action onNext,
            Action<Exception> onError,
            Action onCompleted)
        {
            return source.Subscribe(_ => onNext.SafeInvoke(), onError, onCompleted);
        }

        // == Silent Subscribe ==

        public static IDisposable SilentSubscribe<T>(
            this IObservable<T> source,
            Action onNext)
        {
            return source.Skip(1).Subscribe(onNext.SafeInvoke);
        }

        public static IDisposable SilentSubscribe<T>(
            this IObservable<T> source,
            Action onNext,
            Action<Exception> onError)
        {
            return source.Skip(1).Subscribe(onNext.SafeInvoke, onError: onError);
        }

        public static IDisposable SilentSubscribe<T>(
            this IObservable<T> source,
            Action onNext,
            Action onCompleted)
        {
            return source.Skip(1).Subscribe(onNext.SafeInvoke, onCompleted);
        }

        public static IDisposable SilentSubscribe<T>(
            this IObservable<T> source,
            Action onNext,
            Action<Exception> onError,
            Action onCompleted)
        {
            return source.Skip(1).Subscribe(onNext.SafeInvoke, onError, onCompleted);
        }

        public static IDisposable SilentSubscribe<T>(
            this IObservable<T> source,
            Action<T> onNext)
        {
            return source.Skip(1).Subscribe(onNext);
        }

        public static IDisposable SilentSubscribe<T>(
            this IObservable<T> source,
            Action<T> onNext,
            Action<Exception> onError)
        {
            return source.Skip(1).Subscribe(onNext, onError: onError);
        }

        public static IDisposable SilentSubscribe<T>(
            this IObservable<T> source,
            Action<T> onNext,
            Action onCompleted)
        {
            return source.Skip(1).Subscribe(onNext, onCompleted);
        }

        public static IDisposable SilentSubscribe<T>(
            this IObservable<T> source,
            Action<T> onNext,
            Action<Exception> onError,
            Action onCompleted)
        {
            return source.Skip(1).Subscribe(onNext, onError, onCompleted);
        }
    }
}