﻿using AD.Services.Router;
using UniRx;

namespace Game.Spaceships
{
    public class WeaponSlotSetupVM : ViewModel
    {
        private readonly ReactiveProperty<int> value = new();

        public IReadOnlyReactiveProperty<int> Value => value;

        protected override void InitSubscribes()
        {
        }

        public void SetWeapon(int weaponId)
        {
            value.Value = weaponId;
        }

        public WeaponSlotEM GetEditModel()
        {
            return new WeaponSlotEM
            {
                WeaponId = value.Value
            };
        }
    }
}