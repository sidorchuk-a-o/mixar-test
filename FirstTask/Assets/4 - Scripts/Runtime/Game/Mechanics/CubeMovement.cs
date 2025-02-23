using Game.Input;
using UnityEngine;
using VContainer;

namespace Game
{
    public class CubeMovement : MonoBehaviour
    {
        [SerializeField] private float speed = 0.5f;
        [SerializeField] private float sprintMod = 2;

        private IInputService _inputService;

        [Inject]
        public void Inject(IInputService inputService)
        {
            _inputService = inputService;
        }

        private void Update()
        {
            var sprintValue = _inputService.SprintOn ? sprintMod : 1;
            var speedValue = speed * sprintValue * Time.deltaTime;

            transform.position += speedValue * _inputService.MoveDirection;
        }
    }
}