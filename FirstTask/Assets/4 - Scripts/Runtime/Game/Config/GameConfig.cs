using UnityEngine;

namespace Game
{
    [CreateAssetMenu(menuName = "Game/Configs/Game")]
    public class GameConfig : ScriptableObject
    {
        [SerializeField] private GameObject _cubePrefab;
        [SerializeField] private GameObject _coinPrefab;

        public GameObject CubePrefab => _cubePrefab;
        public GameObject CoinPrefab => _coinPrefab;
    }
}