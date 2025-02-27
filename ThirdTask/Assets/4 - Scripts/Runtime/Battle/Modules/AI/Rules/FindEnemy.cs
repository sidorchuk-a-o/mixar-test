namespace Game.Battle
{
    public class FindEnemy : IRule
    {
        private readonly TargetComponent target;
        private readonly SpaceshipComponent spaceship;

        private readonly BattleState battleState;

        public bool CanExecute => !target.HasTarget;

        public FindEnemy(SpaceshipComponent spaceship, BattleState battleState)
        {
            this.spaceship = spaceship;
            this.battleState = battleState;

            target = spaceship.GetComponent<TargetComponent>();
        }

        public void Execute()
        {
            var enemy = battleState.GetEnemy(spaceship);

            target.SetTarget(enemy);
        }
    }
}