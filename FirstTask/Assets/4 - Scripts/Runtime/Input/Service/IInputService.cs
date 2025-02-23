using UnityEngine;

namespace Game.Input
{
    public interface IInputService
    {
        Vector3 MoveDirection { get; }
        bool SprintOn { get; }

        void SetMoveDirection(Vector3 value);
        void SetSprintState(bool value);
    }
}