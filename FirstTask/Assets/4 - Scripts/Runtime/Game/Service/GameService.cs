using Cysharp.Threading.Tasks;
using UnityEngine.Networking;
using UnityEngine.EventSystems;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.XR.Interaction.Toolkit.Interactors;
using VContainer;
using VContainer.Unity;

namespace Game
{
    public class GameService : IGameService, ITickable
    {
        private readonly GameState _state;
        private readonly GameConfig _config;

        private readonly XRRayInteractor _rayInteractor;
        private readonly ARTrackedImageManager _trackedImageManager;
        private readonly IObjectResolver _resolver;

        public GameService(
            GameState state,
            GameConfig config,
            XRRayInteractor rayInteractor,
            ARTrackedImageManager trackedImageManager,
            IObjectResolver resolver)
        {
            _state = state;
            _config = config;
            _rayInteractor = rayInteractor;
            _trackedImageManager = trackedImageManager;
            _resolver = resolver;

            trackedImageManager.trackablesChanged.AddListener(TrackablesChangedCallback);

            CreateCustomImageLibrary();
        }

        // == Spawn Cube ==

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

        // == Trackables ==

        private async void CreateCustomImageLibrary()
        {
            var library = _trackedImageManager.CreateRuntimeLibrary();

            if (library is MutableRuntimeReferenceImageLibrary mutableLibrary)
            {
                using var request = UnityWebRequestTexture.GetTexture(_config.TrackedImageUrl);

                await request.SendWebRequest();

                var texture = DownloadHandlerTexture.GetContent(request);

                texture.name = "marker";

                mutableLibrary.ScheduleAddImageWithValidationJob(
                    texture: texture,
                    name: texture.name,
                    widthInMeters: 0.2f);
            }

            _trackedImageManager.referenceLibrary = library;
            _trackedImageManager.enabled = true;
        }

        private void TrackablesChangedCallback(ARTrackablesChangedEventArgs<ARTrackedImage> args)
        {
            foreach (var image in args.added)
            {
                _resolver.InjectGameObject(image.gameObject);
            }
        }
    }
}