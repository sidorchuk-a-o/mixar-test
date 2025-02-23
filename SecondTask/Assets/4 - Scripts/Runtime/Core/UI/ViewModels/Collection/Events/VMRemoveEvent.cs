using System;

namespace AD.Services.Router
{
    public readonly struct VMRemoveEvent<TViewModel> : IEquatable<VMRemoveEvent<TViewModel>>
        where TViewModel : ViewModel
    {
        public int Index { get; }
        public TViewModel Value { get; }

        public VMRemoveEvent(int index, TViewModel value)
        {
            Index = index;
            Value = value;
        }

        public bool Equals(VMRemoveEvent<TViewModel> other)
        {
            return Index.Equals(other.Index);
        }

        public override int GetHashCode()
        {
            return Index.GetHashCode();
        }
    }
}