using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Object;

namespace Game.Battle
{
    public class WeaponsManager : IWeaponsManager
    {
        private readonly LayerMask shotLayerMask = LayerMask.GetMask("Spaceship");
        private readonly List<ProjectileInfo> projectiles = new();

        public void RegisterNewProjectile(
            WeaponComponent weapon,
            Vector3 sourcePosition,
            Vector3 targetPosition)
        {
            var direction = (targetPosition - sourcePosition).normalized;
            var rotation = Quaternion.LookRotation(direction);

            var projectileGO = Instantiate(weapon.ProjectilePrefab, sourcePosition, rotation);
            var projectile = projectileGO.GetComponent<ProjectileComponent>();

            projectiles.Add(new(projectile, weapon));
        }

        public void Update()
        {
            const float projectileSpeed = 50f;
            const float maxDistance = 100f * 100f;
            const float projectileLength = .5f * .5f;

            for (var i = projectiles.Count - 1; i >= 0; i--)
            {
                var projectile = projectiles[i];

                if (projectile == null)
                {
                    projectiles.RemoveAt(i);
                    continue;
                }

                var projectileTransform = projectile.Projectile.transform;
                var projectileForward = projectileTransform.forward;

                var prevPosition = projectile.ProjectileLastPosition;
                var newPosition = prevPosition + projectileSpeed * Time.deltaTime * projectileForward;

                projectile.Projectile.transform.position = newPosition;

                projectile.ProjectileLastPosition = newPosition;
                projectile.ProjectileDistanceTravelled += Vector3.SqrMagnitude(prevPosition - newPosition);

                if (projectile.ProjectileDistanceTravelled >= maxDistance)
                {
                    RemoveProjectile(i, projectile);
                    continue;
                }

                var rayLength = Vector3.SqrMagnitude(prevPosition - (newPosition + projectileForward * projectileLength));

                if (Physics.Raycast(
                    origin: prevPosition,
                    direction: projectileForward,
                    hitInfo: out var raycastHit,
                    maxDistance: rayLength,
                    layerMask: shotLayerMask,
                    queryTriggerInteraction: QueryTriggerInteraction.Ignore))
                {
                    var hitCollider = raycastHit.collider;
                    var targetActor = hitCollider.GetComponentInParent<ActorComponent>();

                    if (targetActor)
                    {
                        targetActor.ReceiveDamage(projectile.WeaponOwner.Damage);
                    }

                    RemoveProjectile(i, projectile);
                }
            }
        }

        private void RemoveProjectile(int index, ProjectileInfo projectile)
        {
            Destroy(projectile.Projectile.gameObject);

            projectiles.RemoveAt(index);
        }
    }
}