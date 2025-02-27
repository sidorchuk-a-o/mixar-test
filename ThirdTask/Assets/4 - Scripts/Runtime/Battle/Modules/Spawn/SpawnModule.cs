using System.Linq;
using Game.Spaceships;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using static UnityEngine.Object;

namespace Game.Battle
{
    public class SpawnModule
    {
        private readonly BattleConfig battleConfig;
        private readonly SpaceshipsConfig spaceshipsConfig;
        private readonly BattleState battleState;
        private readonly IObjectResolver resolver;

        private readonly SpawnPoint[] spawnPoints;

        public SpawnModule(
            BattleConfig battleConfig,
            SpaceshipsConfig spaceshipsConfig,
            BattleState battleState,
            IObjectResolver resolver)
        {
            this.battleConfig = battleConfig;
            this.spaceshipsConfig = spaceshipsConfig;
            this.battleState = battleState;
            this.resolver = resolver;

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
            var spaceship = SpawnSpaceship(spaceshipData);

            SetupSpaceship(spaceship, spaceshipData, spaceshipEM);

            resolver.InjectGameObject(spaceship.gameObject);

            return spaceship;
        }

        private SpaceshipComponent SpawnSpaceship(SpaceshipData spaceshipData)
        {
            var spawnPoint = spawnPoints.First(x => x.SpacehipId == spaceshipData.Id);
            var spaceshipPrefab = battleConfig.SpaceshipPrefab;

            return Instantiate(spaceshipPrefab, spawnPoint.transform);
        }

        private void SetupSpaceship(SpaceshipComponent spaceship, SpaceshipData spaceshipData, SpaceshipEM spaceshipEM)
        {
            spaceship.Init(spaceshipData);

            // view
            var view = spaceship.GetComponent<SpaceshipViewComponent>();

            view.CreateView(spaceshipData, spaceship);

            // slots
            var weaponSlots = spaceship.GetComponentInChildren<WeaponSlotsComponent>();
            var moduleSlots = spaceship.GetComponentInChildren<ModuleSlotsComponent>();

            weaponSlots.CreateWeapons(GetWeapons(spaceshipEM.WeaponSlotsEM));
            moduleSlots.CreateModules(GetModules(spaceshipEM.ModuleSlotsEM), spaceship);
        }

        private WeaponData[] GetWeapons(WeaponSlotEM[] weaponSlotsEM)
        {
            return weaponSlotsEM
                .Select(x => spaceshipsConfig.GetWeapon(x.WeaponId))
                .ToArray();
        }

        private ModuleData[] GetModules(ModuleSlotEM[] moduleSlotsEM)
        {
            return moduleSlotsEM
                .Select(x => spaceshipsConfig.GetModules(x.ModuleId))
                .ToArray();
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