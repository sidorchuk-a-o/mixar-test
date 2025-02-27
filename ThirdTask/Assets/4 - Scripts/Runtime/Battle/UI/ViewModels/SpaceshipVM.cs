using AD.Services.Router;
using UniRx;

namespace Game.Battle
{
    public class SpaceshipVM : ViewModel
    {
        public int Id { get; }
        public string Title { get; }

        public float MaxHealth { get; }
        public IReadOnlyReactiveProperty<float> Health { get; }

        public float MaxShield { get; }
        public IReadOnlyReactiveProperty<float> Shield { get; }

        public SpaceshipVM(SpaceshipComponent component)
        {
            Id = component.Id;
            Title = component.Title;
            Health = component.Actor.Health.Value;
            MaxHealth = component.Actor.Health.MaxValue;
            Shield = component.Actor.Shield.Value;
            MaxShield = component.Actor.Shield.MaxValue;
        }

        protected override void InitSubscribes()
        {
        }
    }
}