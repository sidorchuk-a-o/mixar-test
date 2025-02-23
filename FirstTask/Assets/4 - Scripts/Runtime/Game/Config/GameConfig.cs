using UnityEngine;

namespace Game
{
    [CreateAssetMenu(menuName = "Game/Configs/Game")]
    public class GameConfig : ScriptableObject
    {
        [SerializeField] private GameObject _cubePrefab;
        [SerializeField] private GameObject _coinPrefab;
        [SerializeField] private string _trackedImageUrl;

        public GameObject CubePrefab => _cubePrefab;
        public GameObject CoinPrefab => _coinPrefab;
        public string TrackedImageUrl => _trackedImageUrl;
    }
}