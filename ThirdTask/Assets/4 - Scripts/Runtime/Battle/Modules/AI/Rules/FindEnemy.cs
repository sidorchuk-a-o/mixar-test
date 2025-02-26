namespace Game.Battle
{
    public class FindEnemy : IRule
    {
        public bool CanExecute => false;

        public FindEnemy(SpaceshipComponent spaceship, BattleState battleState)
        {
        }

        public void Execute()
        {
        }
    }
}