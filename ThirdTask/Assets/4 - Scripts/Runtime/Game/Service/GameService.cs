using AD.Services.Router;
using Game.Battle;
using Game.Spaceships;

namespace Game
{
    public class GameService : IGameService
    {
        private readonly IRouterService router;
        private readonly IBattleService battleService;

        public GameService(IRouterService router, IBattleService battleService)
        {
            this.router = router;
            this.battleService = battleService;
        }

        public void StartSetupSpaceship()
        {
            router.GoTo<SetupSpaceshipsContainer>();
        }

        public void StartBattle(BattleEM battleEM)
        {
            battleService.StartBattle(battleEM);

            router.GoTo<BattleContainer>();
        }

        public void StopBattle()
        {
            battleService.StopBattle();

            router.GoTo<BattleResultContainer>();
        }
    }
}