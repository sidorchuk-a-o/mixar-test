using System;

namespace AD.Services.Router
{
    public readonly struct VMReplaceEvent<TViewModel> : IEquatable<VMReplaceEvent<TViewModel>>
        where TViewModel : ViewModel
    {
        public int Index { get; }
        public TViewModel Value { get; }

        public VMReplaceEvent(int index, TViewModel value)
        {
            Index = index;
            Value = value;
        }

        public override int GetHashCode()
        {
            return Index.GetHashCode();
        }

        public bool Equals(VMReplaceEvent<TViewModel> other)
        {
            return Index.Equals(other.Index);
        }
    }
}