using System.Collections;
using AD.ToolsCollection;
using Game.Spaceships;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

namespace Game.Battle
{
    public class SpaceshipStateContainer : MonoBehaviour
    {
        [ValueDropdown("Editor_GetSpaceships")]
        [SerializeField] private int spacehipId;

        [SerializeField] private TMP_Text nameText;

        [Header("Actor")]
        [SerializeField] private PropertyBar healthBar;
        [SerializeField] private PropertyBar shieldBar;

        public int SpacehipId => spacehipId;

        public void Init(SpaceshipVM spaceshipVM, CompositeDisp disp)
        {
            nameText.text = spaceshipVM.Title;
            healthBar.Init(spaceshipVM.Health, spaceshipVM.MaxHealth, disp);
            shieldBar.Init(spaceshipVM.Shield, spaceshipVM.MaxShield, disp);
        }

#if UNITY_EDITOR
        public static IEnumerable Editor_GetSpaceships()
        {
            return SpaceshipsConfig.Editor_GetSpaceships();
        }
#endif
    }
}