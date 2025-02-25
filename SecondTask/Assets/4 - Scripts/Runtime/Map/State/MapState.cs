using System.Collections.Generic;
using System.Linq;

namespace Game.Map
{
    public class MapState : IMapState
    {
        private readonly Dictionary<StationId, StationInfo> stations;

        public IReadOnlyDictionary<StationId, StationInfo> Stations => stations;

        public MapState(MapConfig config)
        {
            stations = CreateStations(config);
        }

        public StationInfo GetStation(int stationId)
        {
            return stations.Values.First(x => x.StationId.Id == stationId);
        }

        private Dictionary<StationId, StationInfo> CreateStations(MapConfig config)
        {
            var stations = new Dictionary<StationId, StationInfo>();

            foreach (var lineData in config.Lines)
            {
                foreach (var stationId in lineData.StationIds)
                {
                    var id = new StationId(stationId, lineData.Id);

                    if (stations.ContainsKey(id))
                    {
                        continue;
                    }

                    var stationData = config.GetStation(stationId);

                    var nearbyStationIds = config.Lines
                        .SelectMany(line => GetNearbyStations(line, id))
                        .Distinct()
                        .ToArray();

                    var station = new StationInfo(id, stationData, nearbyStationIds);

                    stations.Add(id, station);
                }
            }

            return stations;
        }

        private IEnumerable<StationId> GetNearbyStations(LineData line, StationId targetStationId)
        {
            var lineId = line.Id;
            var stations = line.StationIds;
            var stationsCount = stations.Length;

            for (var i = 0; i < stationsCount; i++)
            {
                if (stations[i] == targetStationId.Id)
                {
                    if (lineId != targetStationId.LineId)
                    {
                        yield return new(targetStationId.Id, lineId);
                        yield break;
                    }

                    if (i > 0)
                    {
                        yield return new(stations[i - 1], lineId);
                    }

                    if (i < stationsCount - 1)
                    {
                        yield return new(stations[i + 1], lineId);
                    }
                }
            }

            yield break;
        }
    }
}