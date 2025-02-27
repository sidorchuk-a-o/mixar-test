using AD.Services.Router;

namespace Game.Battle
{
    public class BattleResultVM : ViewModel
    {
        public bool HasWinner { get; }
        public string WinnerName { get; }

        public BattleResultVM(BattleResultInfo info)
        {
            HasWinner = info.HasWinner;
            WinnerName = info.WinnerName;
        }

        protected override void InitSubscribes()
        {
        }
    }
}