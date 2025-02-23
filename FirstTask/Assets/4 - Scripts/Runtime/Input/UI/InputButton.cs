using AD.ToolsCollection;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Input
{
    public class InputButton : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private Vector3 _direction;

        private readonly ReactiveProperty<Vector3> _value = new();

        public IReadOnlyReactiveProperty<Vector3> Value => _value;

        private void Awake()
        {
            _button
                .OnPointerDownAsObservable()
                .Subscribe(PointerDownCallback)
                .AddTo(this);

            _button
                .OnPointerUpAsObservable()
                .Subscribe(PointerUpCallback)
                .AddTo(this);
        }

        private void PointerDownCallback()
        {
            _value.Value = _direction;
        }

        private void PointerUpCallback()
        {
            _value.Value = Vector3.zero;
        }
    }
}