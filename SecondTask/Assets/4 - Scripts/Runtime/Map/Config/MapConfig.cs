using System.Collections;
using System.Collections.Generic;
using System.Linq;
using AD.ToolsCollection;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Map
{
    [CreateAssetMenu(menuName = "Game/Map Config")]
    public class MapConfig : ScriptableObject
    {
        [SerializeField, TableList] private StationData[] stations;
        [SerializeField, TableList] private LineData[] lines;

        private Dictionary<int, StationData> stationsCache;
        private Dictionary<int, LineData> linesCache;

        public StationData[] Stations => stations;
        public LineData[] Lines => lines;

        public StationData GetStation(int id)
        {
            stationsCache ??= stations.ToDictionary(x => x.Id, x => x);
            stationsCache.TryGetValue(id, out var data);

            return data;
        }

        public LineData GetLine(int id)
        {
            linesCache ??= lines.ToDictionary(x => x.Id, x => x);
            linesCache.TryGetValue(id, out var data);

            return data;
        }

#if UNITY_EDITOR
        public static IEnumerable Editor_GetStations()
        {
            var config = EditorUtils.LoadAsset<MapConfig>();

            var values = new ValueDropdownList<int>
            {
                { "None", 0 }
            };

            if (config != null)
            {
                foreach (var station in config.Stations)
                {
                    values.Add(station.Name, station.Id);
                }
            }

            return values;
        }
#endif
    }
}