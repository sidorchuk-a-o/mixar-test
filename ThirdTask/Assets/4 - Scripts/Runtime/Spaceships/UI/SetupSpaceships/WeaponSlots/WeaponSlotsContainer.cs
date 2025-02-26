using System.Collections.Generic;
using System.Linq;
using AD.ToolsCollection;
using UnityEngine;

namespace Game.Spaceships
{
    public class WeaponSlotsContainer : MonoBehaviour
    {
        [SerializeField] private WeaponSlot slotPrefab;

        private readonly List<WeaponSlot> slots = new();

        public void Init(WeaponSlotSetupVM[] slotsVM, CompositeDisp disp)
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