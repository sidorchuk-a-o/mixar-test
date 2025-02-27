using Game.Spaceships;
using UniRx;
using UnityEngine;

namespace Game.Battle
{
    public class ShieldComponent : MonoBehaviour
    {
        private ShieldData data;
        private HealthComponent health;

        private float recoveryTime;
        private float recoveryValue;

        private readonly ReactiveProperty<float> currentValue = new();

        public int MaxValue { get; private set; }
        public IReadOnlyReactiveProperty<float> Value => currentValue;
        public IReadOnlyReactiveProperty<bool> IsActive { get; private set; }

        public void Init(ShieldData data)
        {
            this.data = data;

            SetMaxValue(data.Value);
            SetRecoveryMod(0);

            health = GetComponent<HealthComponent>();
            IsActive = Value.Select(x => x > 0).ToReadOnlyReactiveProperty();
        }

        public void AddValue(float value)
        {
            var newValue = currentValue.Value + value;

            currentValue.Value = Mathf.Clamp(newValue, 0f, MaxValue);
        }

        public void SetMaxValue(int value)
        {
            MaxValue = value;
            currentValue.Value = value;
        }

        public void SetRecoveryMod(float value)
        {
            recoveryValue = data.RecoverySpeed + data.RecoverySpeed * value;
        }

        private void Update()
        {
            if (currentValue.Value >= MaxValue || !health.IsAlive.Value)
            {
                return;
            }

            recoveryTime += Time.deltaTime;

            if (recoveryTime < 1f)
            {
                return;
            }

            recoveryTime = 0;

            currentValue.Value = Mathf.Min(MaxValue, currentValue.Value + recoveryValue);
        }
    }
}