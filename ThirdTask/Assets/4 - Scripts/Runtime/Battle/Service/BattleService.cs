using Game.Spaceships;
using VContainer;

namespace Game.Battle
{
    public class BattleService : IBattleService
    {
        private readonly SpawnModule spawnModule;
        private readonly AIModule aiModule;

        private readonly WeaponsManager weaponsManager;

        public BattleService(
            BattleConfig battleConfig,
            SpaceshipsConfig spaceshipsConfig,
            BattleState battleState,
            WeaponsManager weaponsManager,
            IObjectResolver resolver)
        {
            this.weaponsManager = weaponsManager;

            spawnModule = new SpawnModule(battleConfig, spaceshipsConfig, battleState, resolver);
            aiModule = new AIModule(battleState);
        }

        public void StartBattle(BattleEM battleEM)
        {
            spawnModule.SpawnAndSetupSpaceships(battleEM.SpaceshipsEM);
            aiModule.SetupSpaceshipActors();
        }

        public void StopBattle()
        {
            aiModule.RemoveSpaceshipActors();
            spawnModule.DespawnSpaceships();
        }

        public void Tick()
        {
            aiModule.Update();
            weaponsManager.Update();
        }
    }
}