using System.Linq;

namespace Game.Battle
{
    public class Attack : IRule
    {
        private readonly TargetComponent target;
        private readonly WeaponComponent[] weapons;

        public bool CanExecute
        {
            get
            {
                return target.HasTarget
                    && weapons.Any(x => !x.IsRecharging);
            }
        }

        public Attack(SpaceshipComponent spaceship)
        {
            target = spaceship.GetComponent<TargetComponent>();
            weapons = spaceship.GetComponentInChildren<WeaponSlotsComponent>().Weapons;
        }

        public void Execute()
        {
            foreach (var weapon in weapons.Where(x => !x.IsRecharging))
            {
                var targetPosition = target.Value.transform.position;

                weapon.TryShoot(targetPosition);
            }
        }
    }
}