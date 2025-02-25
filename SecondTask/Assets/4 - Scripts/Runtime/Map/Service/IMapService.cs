using System.Collections.Generic;

namespace Game.Map
{
    public interface IMapService
    {
        IReadOnlyList<StationInfo> FindShortPath(int startStationId, int endStationId);
    }
}