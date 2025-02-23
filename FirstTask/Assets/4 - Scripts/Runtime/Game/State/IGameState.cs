using UniRx;
using UnityEngine;

namespace Game
{
    public interface IGameState
    {
        IReadOnlyReactiveCollection<GameObject> Cubes { get; }
    }
}