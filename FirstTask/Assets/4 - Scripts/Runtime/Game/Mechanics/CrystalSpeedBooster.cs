using Game.Input;
using UnityEngine;
using VContainer;

namespace Game
{
    public class CrystalSpeedBooster : MonoBehaviour
    {
        private IInputService _inputService;

        [Inject]
        public void Inject(IInputService inputService)
        {
            _inputService = inputService;

            _inputService.SetSprintState(true);
        }

        private void OnDestroy()
        {
            _inputService.SetSprintState(false);
        }
    }
}