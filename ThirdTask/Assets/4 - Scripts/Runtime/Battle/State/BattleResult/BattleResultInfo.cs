namespace Game.Battle
{
    public class BattleResultInfo
    {
        public bool HasWinner { get; }
        public string WinnerName { get; }

        public BattleResultInfo(SpaceshipComponent winner)
        {
            HasWinner = winner != null;
            WinnerName = winner?.Title;
        }
    }
}