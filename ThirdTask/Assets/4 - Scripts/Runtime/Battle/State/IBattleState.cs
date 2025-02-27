using System.Collections.Generic;

namespace Game.Battle
{
    public interface IBattleState
    {
        BattleResultInfo LastBattleResult { get; }
        IReadOnlyList<SpaceshipComponent> Spaceships { get; }

        void SetBattleResult(BattleResultInfo value);
    }
}