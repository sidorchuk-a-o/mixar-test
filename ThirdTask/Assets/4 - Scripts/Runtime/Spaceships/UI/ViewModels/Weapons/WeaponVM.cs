using AD.Services.Router;

namespace Game.Spaceships
{
    public class WeaponVM : ViewModel
    {
        public int Id { get; }
        public string Title { get; }

        public int Damage { get; }
        public int Recharge { get; }

        public WeaponVM(WeaponData data)
        {
            Id = data.Id;
            Title = data.Title;
            Damage = data.Damage;
            Recharge = data.RechargeTime;
        }

        protected override void InitSubscribes()
        {
        }
    }
}