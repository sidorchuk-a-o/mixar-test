using VContainer.Unity;

namespace Game.Battle
{
    public interface IBattleService : ITickable
    {
        void StartBattle(BattleEM battleEM);
        void StopBattle();
    }
}