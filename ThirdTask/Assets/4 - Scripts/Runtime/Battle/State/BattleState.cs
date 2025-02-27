using System;
using System.Collections.Generic;
using System.Linq;

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

        public SpaceshipComponent GetEnemy(SpaceshipComponent spaceship)
        {
            return spaceships.FirstOrDefault(x => x.Id != spaceship.Id);
        }
    }
}