using System.Linq;
using Game.Spaceships;
using UnityEngine;

namespace Game.Battle
{
    public class WeaponSlotsComponent : MonoBehaviour
    {
        [SerializeField] private WeaponSlotComponent[] slots;

        public WeaponComponent[] Weapons { get; private set; }

        public void CreateWeapons(WeaponData[] weapons)
        {
            for (var i = 0; i < weapons.Length; i++)
            {
                var slot = slots[i];
                var weapon = weapons[i];

                slot.CreateWeapon(weapon);
            }

            Weapons = slots
                .Where(x => x.HasWeapon)
                .Select(x => x.Weapon)
                .ToArray();
        }
    }
}