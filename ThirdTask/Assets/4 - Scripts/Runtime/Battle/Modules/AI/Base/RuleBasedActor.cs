namespace Game.Battle
{
    public class RuleBasedActor
    {
        private readonly IRule[] rules;

        public RuleBasedActor(params IRule[] rules)
        {
            this.rules = rules;
        }

        public void Update()
        {
            foreach (var rule in rules)
            {
                if (rule.CanExecute)
                {
                    rule.Execute();
                    return;
                }
            }
        }
    }
}