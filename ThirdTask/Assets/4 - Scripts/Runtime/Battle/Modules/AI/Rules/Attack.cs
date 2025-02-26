namespace Game.Battle
{
    public class Attack : IRule
    {
        public bool CanExecute => false;

        public Attack(SpaceshipComponent spaceship)
        {
        }

        public void Execute()
        {
        }
    }
}