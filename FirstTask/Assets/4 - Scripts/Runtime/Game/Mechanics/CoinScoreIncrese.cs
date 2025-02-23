using AD.ToolsCollection;
using UnityEngine;
using VContainer;

namespace Game
{
    public class CoinScoreIncrese : MonoBehaviour
    {
        [SerializeField] private AudioSample _takeSfx;

        private IGameState _gameState;

        [Inject]
        public void Inject(IGameState gameState)
        {
            _gameState = gameState;
        }

        private void OnDestroy()
        {
            _gameState.IncreaseScore();

            _takeSfx.Play(transform.position);
        }
    }
}