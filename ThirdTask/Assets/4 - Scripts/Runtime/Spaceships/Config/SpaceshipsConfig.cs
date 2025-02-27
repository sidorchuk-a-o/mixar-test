using System.Collections;
using System.Collections.Generic;
using System.Linq;
using AD.ToolsCollection;
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

        private Dictionary<int, SpaceshipData> spaceshipsCache;
        private Dictionary<int, WeaponData> weaponsCache;
        private Dictionary<int, ModuleData> modulesCache;

        public SpaceshipData[] Spaceships => spaceships;
        public WeaponData[] Weapons => weapons;
        public ModuleData[] Modules => modules;

        public SpaceshipData GetSpaceship(int id)
        {
            spaceshipsCache ??= spaceships.ToDictionary(x => x.Id, x => x);
            spaceshipsCache.TryGetValue(id, out var data);

            return data;
        }

        public WeaponData GetWeapon(int id)
        {
            weaponsCache ??= weapons.ToDictionary(x => x.Id, x => x);
            weaponsCache.TryGetValue(id, out var data);

            return data;
        }

        public ModuleData GetModules(int id)
        {
            modulesCache ??= modules.ToDictionary(x => x.Id, x => x);
            modulesCache.TryGetValue(id, out var data);

            return data;
        }

#if UNITY_EDITOR
        public static IEnumerable Editor_GetSpaceships()
        {
            var config = EditorUtils.LoadAsset<SpaceshipsConfig>();

            var values = new ValueDropdownList<int>
            {
                { "None", 0 }
            };

            if (config != null)
            {
                foreach (var spaceship in config.Spaceships)
                {
                    values.Add(spaceship.Title, spaceship.Id);
                }
            }

            return values;
        }
#endif
    }
}