using System.Linq;
using AD.Services.Router;
using AD.ToolsCollection;
using UniRx;
using UnityEngine;

namespace Game.Spaceships
{
    public class SpaceshipSetupVM : ViewModel
    {
        public int Id { get; }
        public string Title { get; }

        public int HealthValue { get; }
        public int ShieldValue { get; }
        public float RecoverySpeedValue { get; }

        public Sprite Preview { get; }

        public WeaponSlotSetupVM[] WeaponSlotsVM { get; }
        public ModuleSlotSetupVM[] ModuleSlotsVM { get; }

        public SpaceshipSetupVM(SpaceshipData data)
        {
            Id = data.Id;
            Title = data.Title;
            HealthValue = data.Health.Value;
            ShieldValue = data.Shield.Value;
            RecoverySpeedValue = data.Shield.RecoverySpeed;
            Preview = data.SpaceshipPreview;

            WeaponSlotsVM = Enumerable
                .Range(0, data.Weapons.SlotCount)
                .Select(x => new WeaponSlotSetupVM())
                .ToArray();

            ModuleSlotsVM = Enumerable
                .Range(0, data.Modules.SlotCount)
                .Select(x => new ModuleSlotSetupVM())
                .ToArray();
        }

        protected override void InitSubscribes()
        {
            WeaponSlotsVM.ForEach(x => x.AddTo(this));
            ModuleSlotsVM.ForEach(x => x.AddTo(this));
        }
    }
}