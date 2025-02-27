using Game.Spaceships;
using UnityEngine;

namespace Game.Battle
{
    public class ModuleComponent : MonoBehaviour
    {
        public void Init(ModuleData moduleData, SpaceshipComponent spaceship)
        {
            // TODO: handler
            switch (moduleData)
            {
                case AddHealthModule module: ApplyModule(module, spaceship); break;
                case AddShieldModule module: ApplyModule(module, spaceship); break;
                case RechargeBoostModule module: ApplyModule(module, spaceship); break;
                case ShieldRecoveryBoostModule module: ApplyModule(module, spaceship); break;
            }
        }

        private void ApplyModule(AddHealthModule module, SpaceshipComponent spaceship)
        {
            var health = spaceship.Actor.Health;

            health.SetMaxValue(health.MaxValue + module.Value);
        }

        private void ApplyModule(AddShieldModule module, SpaceshipComponent spaceship)
        {
            var shield = spaceship.Actor.Shield;

            shield.SetMaxValue(shield.MaxValue + module.Value);
        }

        private void ApplyModule(RechargeBoostModule module, SpaceshipComponent spaceship)
        {
            var weaponSlots = spaceship.GetComponentInChildren<WeaponSlotsComponent>();

            foreach (var weapon in weaponSlots.Weapons)
            {
                weapon.SetRechargeMod(module.Value);
            }
        }

        private void ApplyModule(ShieldRecoveryBoostModule module, SpaceshipComponent spaceship)
        {
            var shield = spaceship.Actor.Shield;

            shield.SetRecoveryMod(module.Value);
        }
    }
}