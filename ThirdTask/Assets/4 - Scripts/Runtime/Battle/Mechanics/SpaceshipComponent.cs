using Game.Spaceships;
using UnityEngine;

namespace Game.Battle
{
    public class SpaceshipComponent : MonoBehaviour
    {
        public int Id { get; private set; }
        public string Title { get; private set; }

        public HealthComponent Health { get; private set; }
        public ShieldComponent Shield { get; private set; }

        public void Init(SpaceshipData spaceshipData)
        {
            Id = spaceshipData.Id;
            Title = spaceshipData.Title;

            Health = GetComponentInChildren<HealthComponent>();
            Health.Init(spaceshipData.Health);

            Shield = GetComponentInChildren<ShieldComponent>();
            Shield.Init(spaceshipData.Shield);
        }
    }
}