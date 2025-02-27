using AD.ToolsCollection;
using DG.Tweening;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Battle
{
    public class PropertyBar : MonoBehaviour
    {
        [Header("Value")]
        [SerializeField] private TMP_Text valueText;

        [Header("Current")]
        [SerializeField] private Slider currentValue;
        [SerializeField] private Image currentFill;
        [SerializeField] private Color currentColor = Color.white;

        [Header("With Delay")]
        [SerializeField] private Slider withDelayValue;
        [SerializeField] private Image withDelayFill;
        [SerializeField] private Color withDelayColor = Color.white;

        private float maxValue;

        private void Awake()
        {
            currentFill.color = currentColor;
            withDelayFill.color = withDelayColor;
        }

        public void Init(IReadOnlyReactiveProperty<float> property, float maxValue, CompositeDisp disp)
        {
            this.maxValue = maxValue;

            property
                .SilentSubscribe(UpdateView)
                .AddTo(disp);

            UpdateView(maxValue);
        }

        private void UpdateView(float value)
        {
            var percent = value / maxValue;

            valueText.text = $"{Mathf.RoundToInt(value)} / {maxValue}";
            currentValue.value = percent;

            withDelayValue.DOKill();
            withDelayValue.DOValue(percent, 0.3f);
        }

        private void OnValidate()
        {
            if (currentFill)
            {
                currentFill.color = currentColor;
            }

            if (withDelayFill)
            {
                withDelayFill.color = withDelayColor;
            }
        }
    }
}