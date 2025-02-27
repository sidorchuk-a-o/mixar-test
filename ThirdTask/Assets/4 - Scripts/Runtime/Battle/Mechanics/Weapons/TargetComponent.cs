using UnityEngine;

namespace Game.Battle
{
    public class TargetComponent : MonoBehaviour
    {
        public SpaceshipComponent Value { get; private set; }

        public bool HasTarget
        {
            get
            {
                return Value != null
                    && Value.Actor.Health.IsAlive.Value;
            }
        }

        public void SetTarget(SpaceshipComponent value)
        {
            Value = value;
        }
    }
}