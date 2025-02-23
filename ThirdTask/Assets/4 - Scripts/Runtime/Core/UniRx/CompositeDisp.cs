using System.Collections.Generic;
using System;
using UniRx;
using System.Collections;

namespace AD.ToolsCollection
{
    public sealed class CompositeDisp : ICollection<IDisposable>, IDisposable, ICancelable
    {
        private const int shrinkThreshold = 64;

        private readonly object _gate = new();

        private List<IDisposable> disposables;

        public bool IsDisposed { get; private set; }
        public bool IsReadOnly => false;

        public int Count { get; private set; }

        public CompositeDisp()
        {
            disposables = new();
        }

        public void Add(IDisposable item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }

            var shouldDispose = false;

            lock (_gate)
            {
                shouldDispose = IsDisposed;

                if (!IsDisposed)
                {
                    disposables.Add(item);
                    Count++;
                }
            }

            if (shouldDispose)
            {
                item.Dispose();
            }
        }

        public bool Remove(IDisposable item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }

            var shouldDispose = false;

            lock (_gate)
            {
                if (!IsDisposed)
                {
                    var i = disposables.IndexOf(item);

                    if (i >= 0)
                    {
                        shouldDispose = true;

                        disposables[i] = null;
                        Count--;

                        if (disposables.Capacity > shrinkThreshold && Count < disposables.Capacity / 2)
                        {
                            var old = disposables;

                            disposables = new(disposables.Capacity / 2);

                            foreach (var d in old)
                            {
                                if (d != null)
                                {
                                    disposables.Add(d);
                                }
                            }
                        }
                    }
                }
            }

            if (shouldDispose)
            {
                item.Dispose();
            }

            return shouldDispose;
        }

        public void Dispose()
        {
            var currentDisposables = default(IDisposable[]);

            lock (_gate)
            {
                if (!IsDisposed)
                {
                    IsDisposed = true;
                    currentDisposables = disposables.ToArray();

                    disposables.Clear();
                    Count = 0;
                }
            }

            if (currentDisposables != null)
            {
                foreach (var d in currentDisposables)
                {
                    d?.Dispose();
                }
            }
        }

        public void Clear()
        {
            var currentDisposables = default(IDisposable[]);

            lock (_gate)
            {
                currentDisposables = disposables.ToArray();

                disposables.Clear();
                Count = 0;
            }

            foreach (var d in currentDisposables)
            {
                if (d is CompositeDisp cd)
                {
                    cd.Clear();
                }
                else
                {
                    d?.Dispose();
                }
            }
        }

        public bool Contains(IDisposable item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }

            lock (_gate)
            {
                return disposables.Contains(item);
            }
        }

        public void CopyTo(IDisposable[] array, int arrayIndex)
        {
            if (array == null)
            {
                throw new ArgumentNullException("array");
            }

            if (arrayIndex < 0 || arrayIndex >= array.Length)
            {
                throw new ArgumentOutOfRangeException("arrayIndex");
            }

            lock (_gate)
            {
                var disArray = new List<IDisposable>();

                foreach (var item in disposables)
                {
                    if (item != null)
                    {
                        disArray.Add(item);
                    }
                }

                Array.Copy(disArray.ToArray(), 0, array, arrayIndex, array.Length - arrayIndex);
            }
        }

        public IEnumerator<IDisposable> GetEnumerator()
        {
            var res = new List<IDisposable>();

            lock (_gate)
            {
                foreach (var d in disposables)
                {
                    if (d != null)
                    {
                        res.Add(d);
                    }
                }
            }

            return res.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}