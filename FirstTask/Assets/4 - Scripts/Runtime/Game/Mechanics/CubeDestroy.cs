using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using VContainer;

namespace Game
{
    public class CubeDestroy : MonoBehaviour
    {
        [SerializeField] private XRSimpleInteractable _interactable;
        [SerializeField] private float _holdTime = 0.5f;

        private IGameState _gameState;
        private float _destroyTimer;

        [Inject]
        public void Inject(IGameState gameState)
        {
            _gameState = gameState;
        }

        private void Update()
        {
            if (!_interactable.isSelected)
            {
                _destroyTimer = 0;
                return;
            }

            _destroyTimer += Time.deltaTime;

            if (_destroyTimer >= _holdTime)
            {
                _gameState.DestroyCube(gameObject);
            }
        }
    }
}