using System.Collections.Generic;

namespace Game.Map
{
    public interface IMapState
    {
        IReadOnlyDictionary<StationId, StationInfo> Stations { get; }

        StationInfo GetStation(int stationId);
    }
}