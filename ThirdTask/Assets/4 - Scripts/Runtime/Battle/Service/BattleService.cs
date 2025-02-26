using Game.Spaceships;

namespace Game.Battle
{
    public class BattleService : IBattleService
    {
        private readonly SpawnModule spawnModule;
        private readonly AIModule aiModule;

        public BattleService(
            BattleConfig battleConfig,
            SpaceshipsConfig spaceshipsConfig,
            BattleState battleState)
        {
            spawnModule = new SpawnModule(battleConfig, spaceshipsConfig, battleState);
            aiModule = new AIModule(battleState);
        }

        public void StartBattle(BattleEM battleEM)
        {
            spawnModule.SpawnAndSetupSpaceships(battleEM.SpaceshipsEM);
            aiModule.SetupSpaceshipActors();
        }

        public void StopBattle()
        {
            // TODO: зафиксировать результаты боя

            aiModule.RemoveSpaceshipActors();
            spawnModule.DespawnSpaceships();
        }

        public void Tick()
        {
            aiModule.Update();
        }
    }
}