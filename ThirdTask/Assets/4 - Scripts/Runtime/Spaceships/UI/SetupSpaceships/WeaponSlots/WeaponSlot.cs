using AD.Services.Router;
using AD.ToolsCollection;
using TMPro;
using UniRx;
using UnityEngine;
using VContainer;

namespace Game.Spaceships
{
    public class WeaponSlot : MonoBehaviour
    {
        [SerializeField] private TMP_Dropdown weaponDropdown;

        private OptionDataList<int> weaponOptions;
        private WeaponSlotSetupVM slotVM;

        [Inject]
        public void Inject(ISpaceshipsVMFactory spaceshipsVMF)
        {
            weaponOptions = spaceshipsVMF.WeaponOptions;

            weaponDropdown.AddOptions(weaponOptions.Options);
            weaponDropdown.onValueChanged.AsObservable()
                .Subscribe(WeaponChangedCallback)
                .AddTo(this);
        }

        public void Init(WeaponSlotSetupVM slotVM, CompositeDisp disp)
        {
            this.slotVM = slotVM;

            slotVM.SetWeapon(weaponOptions[weaponDropdown.value]);
        }

        private void WeaponChangedCallback(int index)
        {
            slotVM.SetWeapon(weaponOptions[index]);
        }
    }
}