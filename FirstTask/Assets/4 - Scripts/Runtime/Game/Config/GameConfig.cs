using UnityEngine;

namespace Game
{
    [CreateAssetMenu(menuName = "Game/Configs/Game")]
    public class GameConfig : ScriptableObject
    {
        [Header("Cube")]
        [SerializeField] private GameObject _cubePrefab;

        [Header("Coin")]
        [SerializeField] private GameObject _coinPrefab;
        [SerializeField] private int _maxCoinCount;

        [Header("Tracked Image")]
        [SerializeField] private string _trackedImageUrl;

        public GameObject CubePrefab => _cubePrefab;
        public GameObject CoinPrefab => _coinPrefab;
        public int MaxCoinCount => _maxCoinCount;
        public string TrackedImageUrl => _trackedImageUrl;
    }
}