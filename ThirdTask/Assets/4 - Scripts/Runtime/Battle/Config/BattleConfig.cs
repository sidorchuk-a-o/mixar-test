using UnityEngine;

namespace Game.Battle
{
    [CreateAssetMenu(menuName = "Game/Configs/Battle Config")]
    public class BattleConfig : ScriptableObject
    {
        [SerializeField] private SpaceshipComponent spaceshipPrefab;

        public SpaceshipComponent SpaceshipPrefab => spaceshipPrefab;
    }
}