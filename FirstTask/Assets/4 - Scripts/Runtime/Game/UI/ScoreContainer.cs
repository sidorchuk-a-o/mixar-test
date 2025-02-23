using AD.Services.Router;
using TMPro;
using UniRx;
using UnityEngine;
using VContainer;

namespace Game.Input
{
    public class ScoreContainer : UIContainer
    {
        [SerializeField] private TMP_Text scoreText;

        private ScoreVM _scoreVM;

        [Inject]
        public void Inject(IGameVMFactory gameVMF)
        {
            _scoreVM = gameVMF.GetScore();
            _scoreVM.AddTo(disp);

            _scoreVM.ScoreStr
                .Subscribe(x => scoreText.text = x)
                .AddTo(disp);
        }
    }
}