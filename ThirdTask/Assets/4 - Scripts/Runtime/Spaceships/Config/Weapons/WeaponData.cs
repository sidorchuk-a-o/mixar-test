using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Spaceships
{
    [Serializable]
    public class WeaponData
    {
        [SerializeField] private int id;
        [SerializeField] private string title;

        [BoxGroup("Params")]
        [SerializeField] private int damage;

        [BoxGroup("Params")]
        [SerializeField] private int recharge;

        [BoxGroup("Projectile (Prefab)"), HideLabel]
        [PreviewField(60, ObjectFieldAlignment.Left)]
        [SerializeField] private GameObject projectilePrefab;

        public int Id => id;
        public string Title => title;

        public int Damage => damage;
        public int Recharge => recharge;

        public GameObject ProjectilePrefab => projectilePrefab;
    }
}