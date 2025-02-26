using AD.ToolsCollection;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Spaceships
{
    public class SetupSpaceshipContainer : MonoBehaviour
    {
        [SerializeField] private TMP_Text titleText;

        [Header("Params")]
        [SerializeField] private TMP_Text healthText;
        [SerializeField] private string healthFormat;
        [SerializeField] private TMP_Text shieldText;
        [SerializeField] private string shieldFormat;
        [SerializeField] private Image previewImage;

        [Header("Modules")]
        [SerializeField] private WeaponSlotsContainer weaponSlotsContainer;
        [SerializeField] private ModuleSlotsContainer moduleSlotsContainer;

        public void Init(SpaceshipSetupVM spaceshipVM, CompositeDisp disp)
        {
            titleText.text = spaceshipVM.Title;

            healthText.text = string.Format(healthFormat, spaceshipVM.HealthValue);
            shieldText.text = string.Format(shieldFormat, spaceshipVM.ShieldValue, spaceshipVM.RecoverySpeedValue);

            previewImage.sprite = spaceshipVM.Preview;

            weaponSlotsContainer.Init(spaceshipVM.WeaponSlotsVM, disp);
            moduleSlotsContainer.Init(spaceshipVM.ModuleSlotsVM, disp);
        }
    }
}