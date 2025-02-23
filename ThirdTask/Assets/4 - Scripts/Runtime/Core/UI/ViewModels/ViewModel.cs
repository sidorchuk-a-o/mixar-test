using AD.ToolsCollection;
using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using UniRx;

namespace AD.Services.Router
{
    public abstract class ViewModel : IDisposable
    {
        private readonly CompositeDisp disp = new();
        private readonly List<ViewModel> relatedVMs = new();

        // == Subscribes ==

        public void AddTo(CompositeDisp parentDisp)
        {
            ResetSubscribes();

            disp.AddTo(parentDisp);

            InitSubscribes();
        }

        protected abstract void InitSubscribes();

        public virtual void ResetSubscribes()
        {
            relatedVMs.ForEach(x => x.ResetSubscribes());
            relatedVMs.Clear();

            disp.Clear();
        }

        internal void Add(ViewModel relatedVM)
        {
            if (relatedVMs.Contains(relatedVM))
            {
                return;
            }

            relatedVM.AddTo(disp);
            relatedVMs.Add(relatedVM);
        }

        internal void Add(IDisposable disposable)
        {
            disposable.AddTo(disp);
        }

        public void Dispose()
        {
            ResetSubscribes();

            disp.Dispose();
        }
    }
}