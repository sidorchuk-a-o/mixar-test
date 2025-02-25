using System;
using UnityEngine;

namespace Game.Spaceships
{
    [Serializable]
    public class ShieldData
    {
        [SerializeField] private int value;
        [SerializeField] private float recoverySpeed;

        public int Value => value;
        public float RecoverySpeed => recoverySpeed;
    }
}