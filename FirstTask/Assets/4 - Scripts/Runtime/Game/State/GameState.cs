using UniRx;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Game
{
    public class GameState : IGameState
    {
        private readonly GameConfig _config;
        private readonly IObjectResolver _resolver;

        private readonly ReactiveCollection<GameObject> _cubes = new();

        public IReadOnlyReactiveCollection<GameObject> Cubes => _cubes;

        public GameState(GameConfig config, IObjectResolver resolver)
        {
            _config = config;
            _resolver = resolver;
        }

        public void CreateCube(Vector3 spawnPoint)
        {
            var cube = Object.Instantiate(
                original: _config.CubePrefab,
                position: spawnPoint,
                rotation: Quaternion.identity);

            _resolver.InjectGameObject(cube);

            _cubes.Add(cube);
        }

        public void DestroyCube(GameObject cube)
        {
            _cubes.Remove(cube);

            Object.Destroy(cube);
        }
    }
}