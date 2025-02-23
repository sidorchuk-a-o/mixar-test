using System.Collections.Generic;
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

        private readonly List<GameObject> _coins = new();
        private readonly ReactiveProperty<int> _score = new();

        public IReadOnlyReactiveCollection<GameObject> Cubes => _cubes;

        public IReadOnlyList<GameObject> Coins => _coins;
        public IReadOnlyReactiveProperty<int> Score => _score;

        public GameState(GameConfig config, IObjectResolver resolver)
        {
            _config = config;
            _resolver = resolver;
        }

        // == Cube ==

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

        // == Coin ==

        public void CreateCoin(Vector3 spawnPoint)
        {
            if (_coins.Count >= _config.MaxCoinCount)
            {
                return;
            }

            var coin = Object.Instantiate(
                original: _config.CoinPrefab,
                position: spawnPoint,
                rotation: Quaternion.identity);

            _resolver.InjectGameObject(coin);

            _coins.Add(coin);
        }

        public void DestroyCoin(GameObject coin)
        {
            _coins.Remove(coin);

            Object.Destroy(coin);
        }

        public void IncreaseScore()
        {
            _score.Value++;
        }
    }
}