using System;
using UnityEngine;

namespace Game.Spaceships
{
    [Serializable]
    public class WeaponsData
    {
        [SerializeField] private int slotCount;

        public int SlotCount => slotCount;
    }
}