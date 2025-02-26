using System.Collections.Generic;
using System.Linq;
using AD.ToolsCollection;
using UnityEngine;

namespace Game.Spaceships
{
    public class ModuleSlotsContainer : MonoBehaviour
    {
        [SerializeField] private ModuleSlot slotPrefab;

        private readonly List<ModuleSlot> slots = new();

        public void Init(ModuleSlotSetupVM[] slotsVM, CompositeDisp disp)
        {
            for (var i = 0; i < slotsVM.Length; i++)
            {
                var slotVM = slotsVM[i];
                var slot = slots.ElementAtOrDefault(i);

                if (slot == null)
                {
                    slot = Instantiate(slotPrefab, transform);
                    slots.Add(slot);
                }

                slot.Init(slotVM, disp);
            }
        }
    }
}