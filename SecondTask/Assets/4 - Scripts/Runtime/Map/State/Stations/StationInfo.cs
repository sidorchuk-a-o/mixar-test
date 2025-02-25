namespace Game.Map
{
    public class StationInfo
    {
        public string Name { get; }
        public StationId StationId { get; }

        public StationId[] NearbyStationIds { get; }
        public bool IsChecked { get; private set; }

        public StationInfo(StationId stationId, StationData stationData, StationId[] nearbyStationIds)
        {
            StationId = stationId;
            Name = stationData.Name;
            NearbyStationIds = nearbyStationIds;
        }

        public void MarkAsChecked()
        {
            IsChecked = true;
        }

        public void ResetCheck()
        {
            IsChecked = false;
        }
    }
}