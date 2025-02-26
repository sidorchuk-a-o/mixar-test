using Game.Battle;
using VContainer.Unity;

namespace Game
{
    public class EntryPoint : IInitializable, ITickable
    {
        private readonly IGameService gameService;
        private readonly IBattleService battleService;

        public EntryPoint(IGameService gameService, IBattleService battleService)
        {
            this.gameService = gameService;
            this.battleService = battleService;
        }

        void IInitializable.Initialize()
        {
            gameService.StartSetupSpaceship();
        }

        void ITickable.Tick()
        {
            battleService.Tick();
        }
    }
}