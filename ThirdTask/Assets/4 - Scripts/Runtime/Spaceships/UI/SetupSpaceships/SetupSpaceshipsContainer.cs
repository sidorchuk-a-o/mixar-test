using System.Linq;
using AD.Services.Router;
using AD.ToolsCollection;
using Game.Battle;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace Game.Spaceships
{
    public class SetupSpaceshipsContainer : UIContainer
    {
        [Header("Spaceships")]
        [SerializeField] private SetupSpaceshipContainer[] setupContainers;

        [Header("Battle")]
        [SerializeField] private Button startBattleButton;

        private IGameService gameService;
        private ISpaceshipsVMFactory spaceshipsVMF;

        private SpaceshipSetupVM[] setupsVM;

        [Inject]
        public void Inject(IGameService gameService, ISpaceshipsVMFactory spaceshipsVMF)
        {
            this.gameService = gameService;
            this.spaceshipsVMF = spaceshipsVMF;
        }

        private void Awake()
        {
            startBattleButton
                .OnClickAsObservable()
                .Subscribe(StartBattleClickCallback)
                .AddTo(this);
        }

        protected override void OnEnable()
        {
            base.OnEnable();

            setupsVM = spaceshipsVMF.GetSpaceshipSetups();

            for (var i = 0; i < setupsVM.Length; i++)
            {
                var setupVM = setupsVM[i];
                var setupContainer = setupContainers[i];

                setupVM.AddTo(disp);
                setupContainer.Init(setupVM, disp);
            }
        }

        private void StartBattleClickCallback()
        {
            var spaceshipsEM = setupsVM
                .Select(x => x.GetEditModel())
                .ToArray();

            gameService.StartBattle(new BattleEM
            {
                SpaceshipsEM = spaceshipsEM
            });
        }
    }
}