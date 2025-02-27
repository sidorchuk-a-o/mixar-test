using AD.Services.Router;
using AD.ToolsCollection;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace Game.Battle
{
    public class BattleResultContainer : UIContainer
    {
        [Header("Result")]
        [SerializeField] private TMP_Text winnerNameText;
        [SerializeField] private string winnerNameFormat;
        [SerializeField] private string drawText;

        [Header("Setup")]
        [SerializeField] private Button goToSetupButton;

        private IBattleVMFactory battleVMF;
        private IGameService gameService;

        private BattleResultVM resultVM;

        [Inject]
        public void Inject(IBattleVMFactory battleVMF, IGameService gameService)
        {
            this.battleVMF = battleVMF;
            this.gameService = gameService;
        }

        private void Awake()
        {
            goToSetupButton
                .OnClickAsObservable()
                .Subscribe(GoToSetupCallback)
                .AddTo(this);
        }

        protected override void OnEnable()
        {
            base.OnEnable();

            resultVM = battleVMF.GetBattleResult();
            resultVM.AddTo(disp);

            winnerNameText.text = resultVM.HasWinner
                ? string.Format(winnerNameFormat, resultVM.WinnerName)
                : drawText;
        }

        private void GoToSetupCallback()
        {
            gameService.StartSetupSpaceship();
        }
    }
}