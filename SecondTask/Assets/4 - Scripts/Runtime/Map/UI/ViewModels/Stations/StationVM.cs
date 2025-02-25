using AD.Services.Router;
using UniRx;

namespace Game.Map
{
    public class StationVM : ViewModel
    {
        private readonly ReactiveProperty<bool> isSelected = new();
        private readonly ReactiveProperty<bool> isPath = new();

        public int Id { get; }
        public string Name { get; }

        public IReadOnlyReactiveProperty<bool> IsSelected => isSelected;
        public IReadOnlyReactiveProperty<bool> IsPath => isPath;

        public StationVM(StationData data)
        {
            Id = data.Id;
            Name = data.Name;
        }

        protected override void InitSubscribes()
        {
        }

        public void MarkAsSelected()
        {
            isSelected.Value = true;
        }

        public void MarkAsPath()
        {
            isPath.Value = true;
        }

        public void ResetStates()
        {
            isSelected.Value = false;
            isPath.Value = false;
        }
    }
}