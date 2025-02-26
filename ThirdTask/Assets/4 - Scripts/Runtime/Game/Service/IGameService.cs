using Game.Battle;

namespace Game
{
    public interface IGameService
    {
        void StartSetupSpaceship();
        void StartBattle(BattleEM battleEM);
    }
}