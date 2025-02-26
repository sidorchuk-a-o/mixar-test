using System.Linq;
using Game.Spaceships;
using UnityEngine;
using static UnityEngine.Object;

namespace Game.Battle
{
    public class SpawnModule
    {
        private readonly BattleConfig battleConfig;
        private readonly SpaceshipsConfig spaceshipsConfig;
        private readonly BattleState battleState;

        private readonly SpawnPoint[] spawnPoints;

        public SpawnModule(
            BattleConfig battleConfig,
            SpaceshipsConfig spaceshipsConfig,
            BattleState battleState)
        {
            this.battleConfig = battleConfig;
            this.spaceshipsConfig = spaceshipsConfig;
            this.battleState = battleState;

            spawnPoints = FindObjectsByType<SpawnPoint>(FindObjectsSortMode.None);
        }

        public void SpawnAndSetupSpaceships(SpaceshipEM[] spaceshipsEM)
        {
            var spaceships = spaceshipsEM.Select(SpawnAndSetupSpaceship);

            battleState.AddSpaceships(spaceships);
        }

        private SpaceshipComponent SpawnAndSetupSpaceship(SpaceshipEM spaceshipEM, int index)
        {
            var spaceshipData = spaceshipsConfig.GetSpaceship(spaceshipEM.SpaceshipId);
            var spaceship = SpawnSpaceship(index, spaceshipData);

            SetupSpaceship(spaceship, spaceshipData, spaceshipEM);

            return spaceship;
        }

        private SpaceshipComponent SpawnSpaceship(int index, SpaceshipData spaceshipData)
        {
            var spawnPoint = spawnPoints[index].transform;
            var spaceshipPrefab = battleConfig.SpaceshipPrefab;

            var spaceship = Instantiate(spaceshipPrefab, spawnPoint);

            spaceship.Init(spaceshipData);

            return spaceship;
        }

        private void SetupSpaceship(SpaceshipComponent spaceship, SpaceshipData spaceshipData, SpaceshipEM spaceshipEM)
        {
            // components
            var view = spaceship.GetComponent<SpaceshipViewComponent>();

            // init
            view.Init(spaceshipData);
        }

        public void DespawnSpaceships()
        {
            foreach (var spaceship in battleState.Spaceships)
            {
                Destroy(spaceship.gameObject);
            }

            battleState.RemoveSpaceships();
        }
    }
}