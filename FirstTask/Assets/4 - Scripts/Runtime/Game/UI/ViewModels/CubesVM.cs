using AD.Services.Router;
using UniRx;

namespace Game
{
    public class CubesVM : ViewModel
    {
        private readonly IGameState _state;
        private readonly ReactiveProperty<bool> _hasCubes = new();

        public IReadOnlyReactiveProperty<bool> HasCubes => _hasCubes;

        public CubesVM(IGameState state)
        {
            _state = state;
        }

        protected override void InitSubscribes()
        {
            _state.Cubes
                .ObserveCountChanged()
                .Subscribe(x => _hasCubes.Value = x > 0)
                .AddTo(this);
        }
    }
}