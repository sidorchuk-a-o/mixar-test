using AD.Services.Router;
using UniRx;

namespace Game.Spaceships
{
    public class ModuleSlotSetupVM : ViewModel
    {
        private readonly ReactiveProperty<int> value = new();

        public IReadOnlyReactiveProperty<int> Value => value;

        protected override void InitSubscribes()
        {
        }

        public void SetModule(int moduleId)
        {
            value.Value = moduleId;
        }
    }
}