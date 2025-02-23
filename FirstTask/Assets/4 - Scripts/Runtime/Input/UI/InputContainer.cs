using System.Linq;
using AD.Services.Router;
using AD.ToolsCollection;
using UniRx;
using UnityEngine;
using VContainer;

namespace Game.Input
{
    public class InputContainer : UIContainer
    {
        [SerializeField] private InputButton[] _inputButtons;

        private IInputService inputService;

        [Inject]
        public void Inject(IInputService inputService)
        {
            this.inputService = inputService;

            foreach (var inputButton in _inputButtons)
            {
                inputButton.Value
                    .SilentSubscribe(UpdateInputValue)
                    .AddTo(disp);
            }
        }

        private void UpdateInputValue()
        {
            var moveDirection = _inputButtons.Aggregate(
                seed: Vector3.zero,
                func: (r, n) => r += n.Value.Value);

            inputService.SetMoveDirection(moveDirection.normalized);
        }
    }
}