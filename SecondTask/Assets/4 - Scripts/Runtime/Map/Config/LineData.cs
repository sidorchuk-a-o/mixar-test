using System;
using System.Collections;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Map
{
    [Serializable]
    public class LineData
    {
        [SerializeField]
        [TableColumnWidth(60, resizable: false)]
        private int id;

        [SerializeField]
        [TableColumnWidth(80, resizable: false)]
        private Color color;

        [SerializeField]
        [TableColumnWidth(150, resizable: false)]
        [ValueDropdown("Editor_GetStations")]
        private int[] stationIds;

        public int Id => id;
        public Color Color => color;
        public int[] StationIds => stationIds;

#if UNITY_EDITOR
        public static IEnumerable Editor_GetStations()
        {
            return MapConfig.Editor_GetStations();
        }
#endif
    }
}