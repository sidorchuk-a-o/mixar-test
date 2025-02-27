using System.Linq;
using AD.Services.Router;
using AD.ToolsCollection;
using Cysharp.Threading.Tasks;
using Game.Battle;
using Game.Spaceships;
using UniRx;

namespace Game
{
    public class GameService : IGameService
    {
        private readonly IRouterService router;
        private readonly IBattleService battleService;
        private readonly IBattleState battleState;

        public GameService(
            IRouterService router,
            IBattleService battleService,
            IBattleState battleState)
        {
            this.router = router;
            this.battleService = battleService;
            this.battleState = battleState;
        }

        public void StartSetupSpaceship()
        {
            router.GoTo<SetupSpaceshipsContainer>();
        }

        public void StartBattle(BattleEM battleEM)
        {
            battleService.StartBattle(battleEM);

            router.GoTo<BattleContainer>();

            SubscribeToFinishBattle();
        }

        private void SubscribeToFinishBattle()
        {
            foreach (var spaceship in battleState.Spaceships)
            {
                spaceship.Actor.Health.IsAlive
                    .Where(x => x == false)
                    .Subscribe(StopBattle)
                    .AddTo(spaceship);
            }
        }

        public async void StopBattle()
        {
            CreateBattleResult();

            await UniTask.Delay(2000);

            battleService.StopBattle();

            router.GoTo<BattleResultContainer>();
        }

        private void CreateBattleResult()
        {
            var winner = battleState.Spaceships.FirstOrDefault(x =>
            {
                return x.Actor.Health.IsAlive.Value;
            });

            battleState.SetBattleResult(new BattleResultInfo(winner));
        }
    }
}