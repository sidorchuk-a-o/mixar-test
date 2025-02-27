using System.Linq;

namespace Game.Battle
{
    public class BattleVMFactory : IBattleVMFactory
    {
        private readonly IBattleState battleState;

        public BattleVMFactory(IBattleState battleState)
        {
            this.battleState = battleState;
        }

        public BattleResultVM GetBattleResult()
        {
            return new BattleResultVM(battleState.LastBattleResult);
        }

        public SpaceshipVM[] GetSpacehips()
        {
            return battleState.Spaceships
                .Select(x => new SpaceshipVM(x))
                .ToArray();
        }
    }
}