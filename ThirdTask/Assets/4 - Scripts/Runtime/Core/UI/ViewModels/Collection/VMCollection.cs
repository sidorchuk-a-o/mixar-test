using System;
using System.Collections.Generic;
using System.Linq;
using AD.ToolsCollection;
using UniRx;

namespace AD.Services.Router
{
    public abstract class VMCollection<TValue, TViewModel> : ViewModel
        where TViewModel : ViewModel
    {
        private readonly IReadOnlyReactiveCollection<TValue> values;

        private readonly List<TValue> cachedValues = new();
        private readonly List<TViewModel> cachedViewModels = new();

        private readonly Subject<VMAddEvent> onAdd = new();
        private readonly Subject<VMMoveEvent> onMove = new();
        private readonly Subject<VMRemoveEvent<TViewModel>> onRemove = new();
        private readonly Subject<VMReplaceEvent<TViewModel>> onReplace = new();
        private readonly Subject onClear = new();

        public TViewModel this[int index] => GetOrCreate(index);

        public int Count => values.Count;

        protected VMCollection(IReadOnlyReactiveCollection<TValue> values)
        {
            this.values = values;
        }

        protected override void InitSubscribes()
        {
            values.ObserveAdd()
                .Subscribe(CollectionAddCallback)
                .AddTo(this);

            values.ObserveRemove()
                .Subscribe(CollectionRemoveCallback)
                .AddTo(this);

            values.ObserveMove()
                .Subscribe(CollectionMoveCallback)
                .AddTo(this);

            values.ObserveReplace()
                .Subscribe(CollectionReplaceCallback)
                .AddTo(this);

            values.ObserveCountChanged()
                .Where(x => x == 0)
                .Subscribe(CollectionClearCallback)
                .AddTo(this);

            InitViewModels();
        }

        private void InitViewModels()
        {
            // remove old keys

            var removedValues = cachedValues
                .Select((x, i) => (value: x, index: i))
                .Where(x => x.value == null || !values.Contains(x.value))
                .OrderByDescending(x => x.index)
                .ToListPool();

            foreach (var (value, index) in removedValues)
            {
                var vm = cachedViewModels[index];

                vm?.ResetSubscribes();

                cachedValues.RemoveAt(index);
                cachedViewModels.RemoveAt(index);
            }

            removedValues.ReleaseListPool();

            // init

            foreach (var vm in cachedViewModels)
            {
                vm.AddTo(this);
            }
        }

        public bool Contains(TViewModel value)
        {
            return cachedViewModels.Contains(value);
        }

        public TViewModel FirstOrDefault()
        {
            return GetOrCreate(0);
        }

        public TViewModel FirstOrDefault(Func<TValue, bool> predicate)
        {
            return GetOrCreate(predicate);
        }

        public TViewModel LastOrDefault()
        {
            return GetOrCreate(values.Count - 1);
        }

        public TViewModel ElementAtOrDefault(int index)
        {
            return GetOrCreate(index);
        }

        public TViewModel NearbyOrDefault(int index)
        {
            return GetOrCreate(index) ?? GetOrCreate(index - 1);
        }

        // == Create ==

        protected TViewModel GetOrCreate(Func<TValue, bool> predicate)
        {
            var value = values.FirstOrDefault(predicate);

            return value != null ? GetOrCreate(value) : null;
        }

        protected TViewModel GetOrCreate(int index)
        {
            var value = values.ElementAtOrDefault(index);

            return value != null ? GetOrCreate(value) : null;
        }

        private TViewModel GetOrCreate(TValue value)
        {
            var cachedIndex = cachedValues.IndexOf(value);

            if (cachedIndex == -1)
            {
                cachedIndex = cachedValues.Count;

                cachedValues.Add(value);
                cachedViewModels.Add(Create(value));
            }

            var viewModel = cachedViewModels[cachedIndex];

            viewModel.AddTo(this);

            return viewModel;
        }

        protected abstract TViewModel Create(TValue value);

        // == Add / Remove / Move / Replace ==

        private void CollectionAddCallback(CollectionAddEvent<TValue> data)
        {
            CollectionAddCallback(data, quiet: false);
        }

        private void CollectionAddCallback(CollectionAddEvent<TValue> data, bool quiet)
        {
            var index = data.Index;

            if (!quiet)
            {
                onAdd.OnNext(new(index));
            }
        }

        private void CollectionRemoveCallback(CollectionRemoveEvent<TValue> data)
        {
            CollectionRemoveCallback(data, quiet: false);
        }

        private void CollectionRemoveCallback(CollectionRemoveEvent<TValue> data, bool quiet)
        {
            var index = data.Index;
            var value = data.Value;

            TryRemoveCachedValue(value, out var vm);

            if (!quiet)
            {
                onRemove.OnNext(new(index, vm));
            }
        }

        private void CollectionMoveCallback(CollectionMoveEvent<TValue> data)
        {
            var oldIndex = data.OldIndex;
            var newIndex = data.NewIndex;

            CollectionRemoveCallback(new(oldIndex, data.Value), quiet: true);
            CollectionAddCallback(new(newIndex, data.Value), quiet: true);

            onMove.OnNext(new(oldIndex, newIndex));
        }

        private void CollectionReplaceCallback(CollectionReplaceEvent<TValue> data)
        {
            var index = data.Index;
            var value = data.OldValue;

            TryRemoveCachedValue(value, out var vm);

            onReplace.OnNext(new(index, vm));
        }

        private bool TryRemoveCachedValue(TValue value, out TViewModel vm)
        {
            var cachedIndex = cachedValues.IndexOf(value);

            if (cachedIndex != -1)
            {
                vm = cachedViewModels[cachedIndex];
                vm.ResetSubscribes();

                cachedValues.RemoveAt(cachedIndex);
                cachedViewModels.RemoveAt(cachedIndex);

                return true;
            }

            vm = default;
            return false;
        }

        private void CollectionClearCallback()
        {
            cachedViewModels.ForEach(x => x.ResetSubscribes());

            cachedValues.Clear();
            cachedViewModels.Clear();

            onClear.OnNext();
        }

        // == Observes ==

        public IObservable<VMAddEvent> ObserveAdd()
        {
            return onAdd;
        }

        public IObservable<VMMoveEvent> ObserveMove()
        {
            return onMove;
        }

        public IObservable<VMRemoveEvent<TViewModel>> ObserveRemove()
        {
            return onRemove;
        }

        public IObservable<VMReplaceEvent<TViewModel>> ObserveReplace()
        {
            return onReplace;
        }

        public IObservable ObserveClear()
        {
            return onClear;
        }

        public IObservable<int> ObserveCountChanged()
        {
            return values.ObserveCountChanged();
        }
    }
}