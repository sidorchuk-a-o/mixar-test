using Game.Spaceships;
using UniRx;
using UnityEngine;

namespace Game.Battle
{
    public class ShieldComponent : MonoBehaviour
    {
        private ShieldData data;

        private float recoveryTime;
        private float recoveryValue;

        private readonly ReactiveProperty<float> currentValue = new();

        public int MaxValue { get; private set; }
        public IReadOnlyReactiveProperty<float> Value => currentValue;

        public void Init(ShieldData data)
        {
            this.data = data;

            SetMaxValue(data.Value);
            SetRecoveryMod(0);
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

        public void ReceiveDamage(float damage)
        {
            currentValue.Value = Mathf.Max(0f, currentValue.Value - damage);
        }

        private void Update()
        {
            if (currentValue.Value >= MaxValue)
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