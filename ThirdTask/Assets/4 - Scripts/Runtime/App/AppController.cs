using AD.Core;
using AD.Services.Router;
using Game.Battle;
using Game.Spaceships;
using UnityEngine;
using VContainer.Unity;

namespace Game
{
    public class AppController : MonoBehaviour
    {
        [Header("Scope")]
        [SerializeField] private AppScope appScope;

        private void Awake()
        {
            var installers = new IInstaller[]
            {
                new Installer<RouterService>(),
                new Installer<SpaceshipsVMFactory>(),
                new Installer<BattleVMFactory>(),

                new Installer<WeaponsManager>(),

                new Installer<BattleState>(),
                new Installer<BattleService>(),
                new Installer<GameService>(),

                new EntryPointInstaller<EntryPoint>()
            };

            appScope.Install(installers);
        }
    }
}