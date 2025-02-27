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
        [SerializeField] private string desc;

        [BoxGroup("Params")]
        [SerializeField] private int damage;

        [BoxGroup("Params")]
        [SerializeField] private int rechargeTime;

        [BoxGroup("Projectile (Prefab)"), HideLabel]
        [PreviewField(60, ObjectFieldAlignment.Left)]
        [SerializeField] private GameObject projectilePrefab;

        [BoxGroup("Weapon (Prefab)"), HideLabel]
        [PreviewField(60, ObjectFieldAlignment.Left)]
        [SerializeField] private GameObject weaponPrefab;

        public int Id => id;
        public string Title => title;

        public int Damage => damage;
        public int RechargeTime => rechargeTime;

        public GameObject ProjectilePrefab => projectilePrefab;
        public GameObject WeaponPrefab => weaponPrefab;

        public string GetDesc()
        {
            return string.Format(desc, damage, rechargeTime);
        }
    }
}