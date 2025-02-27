using UnityEngine;

namespace Game.Battle
{
    public class WeaponsManager : IWeaponsManager
    {
        public void RegisterNewProjectile(
            WeaponComponent weapon,
            Vector3 sourcePosition,
            Vector3 targetPosition)
        {
            Debug.Log($"PROJECTILE_{weapon.Id}; s:{sourcePosition}; t:{targetPosition}");
        }

        public void Update()
        {
        }
    }
}