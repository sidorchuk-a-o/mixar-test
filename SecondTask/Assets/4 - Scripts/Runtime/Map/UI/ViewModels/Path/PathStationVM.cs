using AD.Services.Router;

namespace Game.Map
{
    public class PathStationVM : ViewModel
    {
        public StationId StationId { get; }

        public StationVM StationVM { get; }
        public LineVM LineVM { get; }

        public PathStationVM(StationInfo info, IMapVMFactory mapVMF)
        {
            StationId = info.StationId;
            StationVM = mapVMF.GetStation(StationId.Id);
            LineVM = mapVMF.GetLine(StationId.LineId);
        }

        protected override void InitSubscribes()
        {
            StationVM.AddTo(this);
            LineVM.AddTo(this);
        }
    }
}