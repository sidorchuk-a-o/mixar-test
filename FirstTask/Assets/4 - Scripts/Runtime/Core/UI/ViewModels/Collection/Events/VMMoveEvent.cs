using System;

namespace AD.Services.Router
{
    public readonly struct VMMoveEvent : IEquatable<VMMoveEvent>
    {
        public int OldIndex { get; }
        public int NewIndex { get; }

        public VMMoveEvent(int oldIndex, int newIndex)
        {
            OldIndex = oldIndex;
            NewIndex = newIndex;
        }

        public bool Equals(VMMoveEvent other)
        {
            return OldIndex.Equals(other.OldIndex)
                && NewIndex.Equals(other.NewIndex);
        }

        public override int GetHashCode()
        {
            return OldIndex.GetHashCode() ^ NewIndex.GetHashCode();
        }
    }
}