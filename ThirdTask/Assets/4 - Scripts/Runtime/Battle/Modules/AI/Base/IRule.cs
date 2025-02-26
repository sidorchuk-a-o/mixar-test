namespace Game.Battle
{
    public interface IRule
    {
        bool CanExecute { get; }

        void Execute();
    }
}