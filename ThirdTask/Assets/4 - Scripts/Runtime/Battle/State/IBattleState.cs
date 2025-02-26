using System.Collections.Generic;

namespace Game.Battle
{
    public interface IBattleState
    {
        IReadOnlyList<SpaceshipComponent> Spaceships { get; }
    }
}