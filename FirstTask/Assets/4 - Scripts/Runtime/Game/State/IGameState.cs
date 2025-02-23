using UniRx;
using UnityEngine;

namespace Game
{
    public interface IGameState
    {
        IReadOnlyReactiveCollection<GameObject> Cubes { get; }
        IReadOnlyReactiveProperty<int> Score { get; }

        void DestroyCube(GameObject cube);
        void DestroyCoin(GameObject coin);

        void IncreaseScore();
    }
}