using AD.Services.Router;
using AD.ToolsCollection;
using TMPro;
using UniRx;
using UnityEngine;
using VContainer;

namespace Game.Spaceships
{
    public class ModuleSlot : MonoBehaviour
    {
        [SerializeField] private TMP_Dropdown moduleDropdown;

        private OptionDataList<int> moduleOptions;
        private ModuleSlotSetupVM slotVM;

        [Inject]
        public void Inject(ISpaceshipsVMFactory spaceshipsVMF)
        {
            moduleOptions = spaceshipsVMF.ModuleOptions;

            moduleDropdown.AddOptions(moduleOptions.Options);
            moduleDropdown.onValueChanged.AsObservable()
                .Subscribe(ModuleChangedCallback)
                .AddTo(this);
        }

        public void Init(ModuleSlotSetupVM slotVM, CompositeDisp disp)
        {
            this.slotVM = slotVM;

            moduleDropdown.value = 0;
        }

        private void ModuleChangedCallback(int index)
        {
            slotVM.SetModule(moduleOptions[index]);
        }
    }
}