using System.Collections.Generic;
using System.Linq;

namespace Game.Battle
{
    public class AIModule
    {
        private readonly BattleState battleState;

        private readonly List<RuleBasedActor> actors = new();

        public AIModule(BattleState battleState)
        {
            this.battleState = battleState;
        }

        public void SetupSpaceshipActors()
        {
            actors.AddRange(battleState.Spaceships.Select(CreateActor));
        }

        private RuleBasedActor CreateActor(SpaceshipComponent spaceship)
        {
            return new RuleBasedActor(
                new FindEnemy(spaceship, battleState),
                new Attack(spaceship));
        }

        public void RemoveSpaceshipActors()
        {
            actors.Clear();
        }

        public void Update()
        {
            foreach (var actor in actors)
            {
                actor.Update();
            }
        }
    }
}