using AD.Services.Router;
using AD.ToolsCollection;
using UniRx;
using UnityEngine;
using VContainer;

namespace Game.Map
{
    public class StationsContainer : UIContainer
    {
        [Header("Stations")]
        [SerializeField] private StationButton[] stationButtons;

        [Header("Path")]
        [SerializeField] private PathContainer pathContainer;

        private IMapVMFactory mapVMF;
        private StationsVM stationsVM;

        private StationVM startStationVM;
        private StationVM endStationVM;
        private PathVM pathVM;

        [Inject]
        public void Inject(IMapVMFactory mapVMF)
        {
            this.mapVMF = mapVMF;

            stationsVM = mapVMF.GetStations();
            stationsVM.AddTo(disp);

            InitStationButtons();
        }

        private void Awake()
        {
            foreach (var button in stationButtons)
            {
                button.OnSelect
                    .Subscribe(StationSelectedCallback)
                    .AddTo(this);
            }
        }

        private void InitStationButtons()
        {
            foreach (var button in stationButtons)
            {
                var stationVM = stationsVM[button.StationId];

                button.Init(stationVM, disp);
            }
        }

        private void StationSelectedCallback(StationVM stationVM)
        {
            if (startStationVM != null && endStationVM != null)
            {
                pathVM?.Dispose();
                pathVM = null;

                endStationVM = null;
                startStationVM = null;

                stationsVM.ResetStations();
                pathContainer.ResetPath();
            }

            if (startStationVM == null)
            {
                startStationVM = stationVM;
            }
            else
            {
                endStationVM = stationVM;

                // find path

                pathVM = mapVMF.FindShortData(startStationVM.Id, endStationVM.Id);
                pathVM.AddTo(disp);

                // show path
                stationsVM.ApplyPath(pathVM);

                pathContainer.Init(pathVM);
            }

            stationVM.MarkAsSelected();
        }
    }
}