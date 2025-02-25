using System.Collections.Generic;
using System.Linq;
using AD.Services.Router;
using AD.ToolsCollection;

namespace Game.Map
{
    public class PathVM : ViewModel
    {
        public int Count => Values.Length;
        public int TransferCount { get; }

        public PathStationVM[] Values { get; }
        public PathStationVM this[int index] => Values[index];

        public PathVM(IReadOnlyList<StationInfo> path, IMapVMFactory mapVMF)
        {
            Values = path
                .Select(x => new PathStationVM(x, mapVMF))
                .ToArray();

            TransferCount = CalcTransferCount(path);
        }

        private int CalcTransferCount(IReadOnlyList<StationInfo> path)
        {
            var transferCount = 0;
            var lineId = path[0].StationId.LineId;

            foreach (var station in path)
            {
                if (lineId == station.StationId.LineId)
                {
                    continue;
                }

                lineId = station.StationId.LineId;

                transferCount++;
            }

            return transferCount;
        }

        protected override void InitSubscribes()
        {
            Values.ForEach(x => x.AddTo(this));
        }
    }
}