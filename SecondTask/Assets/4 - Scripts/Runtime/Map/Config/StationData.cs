using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Map
{
    [Serializable]
    public class StationData
    {
        [SerializeField]
        [TableColumnWidth(60, resizable: false)]
        private int id;

        [SerializeField]
        [TableColumnWidth(60, resizable: false)]
        private string name;

        public int Id => id;
        public string Name => name;
    }
}