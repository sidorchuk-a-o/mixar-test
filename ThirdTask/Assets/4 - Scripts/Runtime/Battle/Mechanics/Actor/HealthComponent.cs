using Game.Spaceships;
using UniRx;
using UnityEngine;

namespace Game.Battle
{
    public class HealthComponent : MonoBehaviour
    {
        private readonly ReactiveProperty<float> currentValue = new();

        public int MaxValue { get; private set; }
        public IReadOnlyReactiveProperty<float> Value => currentValue;
        public IReadOnlyReactiveProperty<bool> IsAlive { get; private set; }

        public void Init(HealthData healthData)
        {
            SetMaxValue(healthData.Value);

            IsAlive = Value.Select(x => x > 0).ToReadOnlyReactiveProperty();
        }

        public void SetMaxValue(int value)
        {
            MaxValue = value;
            currentValue.Value = value;
        }

        public void ReceiveDamage(float damage)
        {
            currentValue.Value = Mathf.Max(0f, currentValue.Value - damage);
        }
    }
}