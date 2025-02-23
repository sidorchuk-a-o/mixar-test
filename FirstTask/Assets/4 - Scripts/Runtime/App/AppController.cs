using Game.Input;
using UnityEngine;
using VContainer.Unity;

namespace Game
{
    public class AppController : MonoBehaviour
    {
        [Header("Scope")]
        [SerializeField] private AppScope _appScope;

        private void Start()
        {
            var installers = new IInstaller[]
            {
                new Installer<GameState>(),
                new EntryPointInstaller<GameService>(),
                new Installer<GameVMFactory>(),

                new Installer<InputService>()
            };

            _appScope.Install(installers);
        }
    }
}