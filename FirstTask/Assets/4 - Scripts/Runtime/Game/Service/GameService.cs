using UnityEngine.EventSystems;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.XR.Interaction.Toolkit.Interactors;
using VContainer.Unity;

namespace Game
{
    public class GameService : IGameService, ITickable
    {
        private readonly GameState _state;
        private readonly XRRayInteractor _rayInteractor;

        public GameService(GameState state, XRRayInteractor rayInteractor)
        {
            _state = state;
            _rayInteractor = rayInteractor;
        }

        void ITickable.Tick()
        {
            TrySpawnCube();
        }

        private void TrySpawnCube()
        {
            if (EventSystem.current != null &&
                EventSystem.current.IsPointerOverGameObject(-1))
            {
                return;
            }

            if (!_rayInteractor.logicalSelectState.wasPerformedThisFrame ||
                !_rayInteractor.TryGetCurrentARRaycastHit(out var arHit) ||
                (_rayInteractor.TryGetCurrent3DRaycastHit(out var hit) && hit.rigidbody != null))
            {
                return;
            }

            if (arHit.trackable is not ARPlane plane ||
                plane.alignment != PlaneAlignment.HorizontalUp)
            {
                return;
            }

            _state.CreateCube(arHit.pose.position);
        }
    }
}