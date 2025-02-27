using Game.Spaceships;
using UnityEngine;

namespace Game.Battle
{
    public class ModuleSlotComponent : MonoBehaviour
    {
        public void CreateModule(ModuleData moduleData, SpaceshipComponent spaceship)
        {
            if (moduleData == null)
            {
                return;
            }

            var moduleGO = Instantiate(moduleData.ModulePrefab, transform);
            var module = moduleGO.GetComponent<ModuleComponent>();

            module.Init(moduleData, spaceship);
        }
    }
}