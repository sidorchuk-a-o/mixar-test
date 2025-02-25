using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Spaceships
{
    [CreateAssetMenu(menuName = "Game/Configs/Spaceship Config")]
    public class SpaceshipsConfig : ScriptableObject
    {
        [SerializeField] private SpaceshipData[] spaceships;
        [SerializeField] private WeaponData[] weapons;

        [InlineEditor(InlineEditorModes.GUIOnly)]
        [SerializeField] private ModuleData[] modules;

        public SpaceshipData[] Spaceships => spaceships;
        public WeaponData[] Weapons => weapons;
        public ModuleData[] Modules => modules;
    }
}