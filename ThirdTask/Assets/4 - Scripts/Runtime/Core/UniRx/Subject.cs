using System;
using UniRx;

namespace AD.ToolsCollection
{
    public class Subject : IObservable, IDisposable
    {
        private readonly Subject<object> subject = new();

        public IDisposable Subscribe(IObserver<object> observer)
        {
            return subject.Subscribe(observer);
        }

        public void OnNext()
        {
            subject.OnNext(null);
        }

        public void OnCompleted()
        {
            subject.OnCompleted();
        }

        public void OnError(Exception error)
        {
            subject.OnError(error);
        }

        public void Dispose()
        {
            subject.Dispose();
        }
    }
}