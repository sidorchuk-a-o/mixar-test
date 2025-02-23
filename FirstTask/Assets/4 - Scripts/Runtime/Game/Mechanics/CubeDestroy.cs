using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using VContainer;

namespace Game
{
    public class CubeDestroy : MonoBehaviour
    {
        [SerializeField] private XRSimpleInteractable _interactable;
        [SerializeField] private float _holdTime = 0.5f;

        private IGameState _gameState;

        private bool _isSelected;
        private float _destroyTimer;

        [Inject]
        public void Inject(IGameState gameState)
        {
            _gameState = gameState;
        }

        private void Awake()
        {
            _interactable.selectEntered.AddListener(OnSelect);
            _interactable.selectExited.AddListener(OnDeselect);
        }

        private void OnDestroy()
        {
            _interactable.selectEntered.RemoveListener(OnSelect);
            _interactable.selectExited.RemoveListener(OnDeselect);
        }

        private void OnSelect(SelectEnterEventArgs args)
        {
            _isSelected = true;
        }

        private void OnDeselect(SelectExitEventArgs args)
        {
            _isSelected = false;
        }

        private void Update()
        {
            if (!_isSelected)
            {
                _destroyTimer = 0;
                return;
            }

            _destroyTimer += Time.deltaTime;

            if (_destroyTimer >= _holdTime)
            {
                _isSelected = false;

                _gameState.DestroyCube(gameObject);
            }
        }
    }
}