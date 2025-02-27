using Game.Spaceships;
using UnityEngine;

namespace Game.Battle
{
    public class ModuleSlotsComponent : MonoBehaviour
    {
        [SerializeField] private ModuleSlotComponent[] slots;

        public void CreateModules(ModuleData[] modules, SpaceshipComponent spaceship)
        {
            for (var i = 0; i < modules.Length; i++)
            {
                var slot = slots[i];
                var module = modules[i];

                slot.CreateModule(module, spaceship);
            }
        }
    }
}