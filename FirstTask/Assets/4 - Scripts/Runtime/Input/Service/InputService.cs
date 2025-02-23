using UnityEngine;

namespace Game.Input
{
    public class InputService : IInputService
    {
        public Vector3 MoveDirection { get; private set; }
        public bool SprintOn { get; private set; }

        public void SetMoveDirection(Vector3 value)
        {
            MoveDirection = value;
        }

        public void SetSprintState(bool value)
        {
            SprintOn = value;
        }
    }
}