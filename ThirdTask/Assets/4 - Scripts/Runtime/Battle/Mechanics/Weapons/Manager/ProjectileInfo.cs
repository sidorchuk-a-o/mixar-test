using UnityEngine;

namespace Game.Battle
{
    public class ProjectileInfo
    {
        public ProjectileComponent Projectile { get; }
        public WeaponComponent WeaponOwner { get; }

        public Vector3 ProjectileOrigin { get; set; }
        public Vector3 ProjectileLastPosition { get; set; }
        public float ProjectileDistanceTravelled { get; set; }

        public ProjectileInfo(ProjectileComponent projectile, WeaponComponent weaponOwner)
        {
            Projectile = projectile;
            WeaponOwner = weaponOwner;
            ProjectileOrigin = projectile.transform.position;
            ProjectileLastPosition = ProjectileOrigin;
        }
    }
}