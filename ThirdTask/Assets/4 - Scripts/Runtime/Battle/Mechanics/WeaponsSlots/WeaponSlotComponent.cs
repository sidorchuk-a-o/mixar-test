using Game.Spaceships;
using UnityEngine;

namespace Game.Battle
{
    public class WeaponSlotComponent : MonoBehaviour
    {
        public bool HasWeapon { get; private set; }
        public WeaponComponent Weapon { get; private set; }

        public void CreateWeapon(WeaponData weaponData)
        {
            if (weaponData == null)
            {
                return;
            }

            var weaponGO = Instantiate(weaponData.WeaponPrefab, transform);

            Weapon = weaponGO.GetComponent<WeaponComponent>();
            Weapon.Init(weaponData);

            HasWeapon = true;
        }
    }
}