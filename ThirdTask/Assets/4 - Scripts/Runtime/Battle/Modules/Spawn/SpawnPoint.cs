using System.Collections;
using Game.Spaceships;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Battle
{
    public class SpawnPoint : MonoBehaviour
    {
        [ValueDropdown("Editor_GetSpaceships")]
        [SerializeField] private int spacehipId;

        public int SpacehipId => spacehipId;

#if UNITY_EDITOR
        public static IEnumerable Editor_GetSpaceships()
        {
            return SpaceshipsConfig.Editor_GetSpaceships();
        }
#endif
    }
}