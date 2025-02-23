using AD.Services.Router;
using AD.ToolsCollection;
using DG.Tweening;
using UniRx;
using UnityEngine;
using VContainer;

namespace Game
{
    public class GameContainer : UIContainer
    {
        [SerializeField] private CanvasGroup _inputContainer;
        [SerializeField] private CanvasGroup _scoreContainer;

        private CubesVM _cubesVM;
        private ScoreVM _scoreVM;

        private void Awake()
        {
            _inputContainer.alpha = 0;
            _inputContainer.interactable = false;

            _scoreContainer.alpha = 0;
            _scoreContainer.interactable = false;
        }

        [Inject]
        public void Init(IGameVMFactory gameVMF)
        {
            _cubesVM = gameVMF.GetCubes();
            _cubesVM.AddTo(disp);

            _scoreVM = gameVMF.GetScore();
            _scoreVM.AddTo(disp);

            _cubesVM.HasCubes
                .Subscribe(x => StartFade(_inputContainer, x))
                .AddTo(disp);

            _scoreVM.HasScore
                .Subscribe(x => StartFade(_scoreContainer, x))
                .AddTo(disp);
        }

        private void StartFade(CanvasGroup container, bool state)
        {
            container.DOKill();
            container.DOFade(state ? 1 : 0, 0.25f);

            container.interactable = state;
        }
    }
}