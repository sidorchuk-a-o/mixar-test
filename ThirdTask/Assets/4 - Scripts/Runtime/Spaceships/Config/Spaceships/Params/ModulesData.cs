using System;
using UnityEngine;

namespace Game.Spaceships
{
    [Serializable]
    public class ModulesData
    {
        [SerializeField] private int slotCount;

        public int SlotCount => slotCount;
    }
}