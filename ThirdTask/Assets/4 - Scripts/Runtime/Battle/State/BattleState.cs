using System.Collections.Generic;

namespace Game.Battle
{
    public class BattleState : IBattleState
    {
        private readonly List<SpaceshipComponent> spaceships = new();

        public IReadOnlyList<SpaceshipComponent> Spaceships => spaceships;

        public void AddSpaceships(IEnumerable<SpaceshipComponent> values)
        {
            spaceships.AddRange(values);
        }

        public void RemoveSpaceships()
        {
            spaceships.Clear();
        }
    }
}