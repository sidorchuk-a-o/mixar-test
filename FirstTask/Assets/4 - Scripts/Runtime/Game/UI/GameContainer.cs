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

        private CubesVM _cubesVM;

        private void Awake()
        {
            _inputContainer.alpha = 0;
            _inputContainer.interactable = false;
        }

        [Inject]
        public void Init(IGameVMFactory gameVMF)
        {
            _cubesVM = gameVMF.GetCubes();
            _cubesVM.AddTo(disp);

            _cubesVM.HasCubes
                .Subscribe(HasCubesChangedCallback)
                .AddTo(disp);
        }

        private void HasCubesChangedCallback(bool state)
        {
            _inputContainer.DOKill();
            _inputContainer.DOFade(state ? 1 : 0, 0.25f);

            _inputContainer.interactable = state;
        }
    }
}