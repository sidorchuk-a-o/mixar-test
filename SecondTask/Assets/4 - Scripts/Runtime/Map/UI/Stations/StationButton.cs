using System;
using System.Collections;
using AD.Services.Router;
using AD.ToolsCollection;
using Sirenix.OdinInspector;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Map
{
    public class StationButton : MonoBehaviour
    {
        [Header("Station")]
        [SerializeField]
        [ValueDropdown("Editor_GetStations")]
        private int stationId;

        [SerializeField] private TMP_Text nameText;

        [Header("Button")]
        [SerializeField] private Button button;
        [SerializeField] private Image backgroundImage;
        [SerializeField] private Color defaultColor = Color.white;
        [SerializeField] private Color selectedColor = Color.white;
        [SerializeField] private Color pathColor = Color.white;

        private readonly Subject<StationVM> onSelect = new();

        private StationVM stationVM;

        public int StationId => stationId;
        public IObservable<StationVM> OnSelect => onSelect;

        private void Awake()
        {
            button
                .OnClickAsObservable()
                .Subscribe(ClickCallback)
                .AddTo(this);
        }

        public void Init(StationVM stationVM, CompositeDisp disp)
        {
            this.stationVM = stationVM;

            nameText.text = stationVM.Name;

            stationVM.IsPath
                .SilentSubscribe(UpdateColorsState)
                .AddTo(disp);

            stationVM.IsSelected
                .SilentSubscribe(UpdateColorsState)
                .AddTo(disp);

            UpdateColorsState();
        }

        private void ClickCallback()
        {
            onSelect.OnNext(stationVM);
        }

        private void UpdateColorsState()
        {
            var isPath = stationVM.IsPath.Value;
            var isSelected = stationVM.IsSelected.Value;

            var color = isSelected
                ? selectedColor
                : isPath
                    ? pathColor
                    : defaultColor;

            backgroundImage.color = color;
        }

#if UNITY_EDITOR
        public static IEnumerable Editor_GetStations()
        {
            return MapConfig.Editor_GetStations();
        }
#endif
    }
}