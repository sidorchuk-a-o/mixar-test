using AD.Services.Router;
using UniRx;

namespace Game
{
    public class ScoreVM : ViewModel
    {
        private readonly IGameState _state;

        private readonly ReactiveProperty<bool> hasScore = new();
        private readonly ReactiveProperty<string> scoreStr = new();

        public IReadOnlyReactiveProperty<bool> HasScore => hasScore;
        public IReadOnlyReactiveProperty<string> ScoreStr => scoreStr;

        public ScoreVM(IGameState state)
        {
            _state = state;
        }

        protected override void InitSubscribes()
        {
            _state.Score
                .Subscribe(ScoreChangedCallback)
                .AddTo(this);
        }

        private void ScoreChangedCallback(int score)
        {
            scoreStr.Value = score.ToString();
            hasScore.Value = score > 0;
        }
    }
}