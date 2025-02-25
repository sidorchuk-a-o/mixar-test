using System.Collections.Generic;
using System.Linq;
using AD.Services.Router;
using AD.ToolsCollection;
using UniRx;

namespace Game.Map
{
    public class StationsVM : ViewModel
    {
        private readonly Dictionary<int, StationVM> stations;

        public StationVM this[int id] => stations[id];

        public StationsVM(StationData[] stations)
        {
            this.stations = stations.ToDictionary(x => x.Id, x => new StationVM(x));
        }

        protected override void InitSubscribes()
        {
            stations.Values.ForEach(x => x.AddTo(this));
        }

        public void ResetStations()
        {
            stations.Values.ForEach(x => x.ResetStates());
        }

        public void ApplyPath(PathVM pathVM)
        {
            foreach (var station in pathVM.Values)
            {
                var stationVM = stations[station.StationId.Id];

                stationVM.MarkAsPath();
            }
        }
    }
}