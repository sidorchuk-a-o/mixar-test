using System;
using UnityEngine;

namespace Game.Spaceships
{
    [Serializable]
    public class HealthData
    {
        [SerializeField] private int value;

        public int Value => value;
    }
}