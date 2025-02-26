using System.Collections.Generic;
using System.Linq;
using AD.ToolsCollection;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Game.Spaceships
{
    public class WeaponSlotsContainer : MonoBehaviour
    {
        [SerializeField] private WeaponSlot slotPrefab;

        private IObjectResolver resolver;
        private readonly List<WeaponSlot> slots = new();

        [Inject]
        public void Inject(IObjectResolver resolver)
        {
            this.resolver = resolver;
        }

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
                    resolver.InjectGameObject(slot.gameObject);
                }

                slot.Init(slotVM, disp);
            }
        }
    }
}