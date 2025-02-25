namespace Game.Map
{
    public interface IMapVMFactory
    {
        LineVM GetLine(int id);
        StationVM GetStation(int id);
        StationsVM GetStations();

        PathVM FindShortData(int startStationId, int endStationId);
    }
}