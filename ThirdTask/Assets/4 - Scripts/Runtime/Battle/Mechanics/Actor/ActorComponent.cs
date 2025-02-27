using Game.Spaceships;
using UnityEngine;

namespace Game.Battle
{
    public class ActorComponent : MonoBehaviour
    {
        public HealthComponent Health { get; private set; }
        public ShieldComponent Shield { get; private set; }

        public void Init(HealthData health, ShieldData shield)
        {
            Health = GetComponent<HealthComponent>();
            Health.Init(health);

            Shield = GetComponent<ShieldComponent>();
            Shield.Init(shield);
        }

        public void ReceiveDamage(float damage)
        {
            if (Shield && Shield.IsActive.Value)
            {
                var shield = Shield.Value.Value;

                Shield.AddValue(-damage);

                var v = Mathf.Max(damage - shield, 0);
                damage = v;
            }

            if (Health && Health.IsAlive.Value)
            {
                Health.AddValue(-damage);
            }
        }
    }
}