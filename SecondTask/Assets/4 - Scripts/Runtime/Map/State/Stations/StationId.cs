using System;

namespace Game.Map
{
    public readonly struct StationId : IEquatable<StationId>
    {
        public int Id { get; }
        public int LineId { get; }

        public StationId(int stationId, int lineId)
        {
            Id = stationId;
            LineId = lineId;
        }

        // == IEquatable ==

        public bool Equals(StationId other)
        {
            return Id == other.Id
                && LineId == other.LineId;
        }

        public override bool Equals(object obj)
        {
            return obj is StationId id
                && Equals(id);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, LineId);
        }

        public override string ToString()
        {
            return $"Line: {LineId}; Station: {Id}";
        }
    }
}