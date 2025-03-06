using System.Collections.Generic;
using System.Linq;
using AD.ToolsCollection;
using Sirenix.Utilities;
using UnityEngine.Pool;

namespace Game.Map
{
    public class MapService : IMapService
    {
        private readonly MapState state;

        public MapService(MapState state)
        {
            this.state = state;
        }

        public IReadOnlyList<StationInfo> FindShortPath(int startStationId, int endStationId)
        {
            var path = new List<StationInfo>();
            var endNodes = ListPool<PathNode>.Get();

            // search end nodes
            FindEndCandidates(startStationId, endStationId, endNodes);

            // find path
            FindShortPath(path, endNodes);

            // reset
            state.Stations.Values.ForEach(x => x.ResetCheck());

            endNodes.ReleaseListPool();

            return path;
        }

        /// <summary>
        /// BFS
        /// </summary>
        private void FindEndCandidates(int startStationId, int endStationId, List<PathNode> endNodes)
        {
            var nodesQueue = new Queue<PathNode>();
            var startStation = state.GetStation(startStationId);

            var startNode = new PathNode
            {
                Station = startStation
            };

            nodesQueue.Enqueue(startNode);

            while (nodesQueue.Count > 0)
            {
                var currentNode = nodesQueue.Dequeue();

                currentNode.Station.MarkAsChecked();

                if (currentNode.Station.StationId.Id == endStationId)
                {
                    endNodes.Add(currentNode);
                }

                foreach (var nearbyStationId in currentNode.Station.NearbyStationIds)
                {
                    var nearbyStation = state.Stations[nearbyStationId];

                    if (nearbyStation.IsChecked)
                    {
                        continue;
                    }

                    nearbyStation.MarkAsChecked();

                    var isStart = nearbyStation.StationId.Id == startStationId;

                    var nearbyNode = new PathNode
                    {
                        Station = nearbyStation,
                        Prev = isStart ? null : currentNode,
                        Weight = isStart ? 0 : currentNode.Weight + 1
                    };

                    nodesQueue.Enqueue(nearbyNode);
                }
            }
        }

        private static void FindShortPath(List<StationInfo> path, List<PathNode> endNodes)
        {
            var node = endNodes
                .OrderBy(x => x.Weight)
                .First();

            while (node.Prev != null)
            {
                path.Insert(0, node.Station);

                node = node.Prev;
            }

            path.Insert(0, node.Station);
        }

        private class PathNode
        {
            public int Weight;
            public StationInfo Station;

            public PathNode Prev;
        }
    }
}