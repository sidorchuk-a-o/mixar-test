using UnityEngine;
using VContainer;

namespace Game
{
    public class CoinDestroyTrigger : MonoBehaviour
    {
        private IGameState _gameState;

        [Inject]
        public void Inject(IGameState gameState)
        {
            _gameState = gameState;
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                _gameState.DestroyCoin(gameObject);
            }
        }
    }
}