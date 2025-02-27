using UnityEngine;

namespace Game.Battle
{
    public interface IWeaponsManager
    {
        void RegisterNewProjectile(
            WeaponComponent weapon,
            Vector3 sourcePosition,
            Vector3 targetPosition);
    }
}