namespace Game.Map
{
    public class MapVMFactory : IMapVMFactory
    {
        private readonly MapConfig config;
        private readonly IMapService service;

        public MapVMFactory(MapConfig config, IMapService service)
        {
            this.config = config;
            this.service = service;
        }

        public LineVM GetLine(int id)
        {
            return new LineVM(config.GetLine(id));
        }

        public StationVM GetStation(int id)
        {
            return new StationVM(config.GetStation(id));
        }

        public StationsVM GetStations()
        {
            return new StationsVM(config.Stations);
        }

        public PathVM FindShortData(int startStationId, int endStationId)
        {
            var path = service.FindShortPath(startStationId, endStationId);

            return new PathVM(path, this);
        }
    }
}