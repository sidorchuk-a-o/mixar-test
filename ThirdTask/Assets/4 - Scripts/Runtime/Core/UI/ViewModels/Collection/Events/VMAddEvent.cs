using System;

namespace AD.Services.Router
{
    public readonly struct VMAddEvent : IEquatable<VMAddEvent>
    {
        public int Index { get; }

        public VMAddEvent(int index)
        {
            Index = index;
        }

        public bool Equals(VMAddEvent other)
        {
            return Index.Equals(other.Index);
        }

        public override int GetHashCode()
        {
            return Index.GetHashCode();
        }
    }
}