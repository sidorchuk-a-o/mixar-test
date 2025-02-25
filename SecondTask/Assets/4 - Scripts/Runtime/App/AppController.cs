using AD.Core;
using Game.Map;
using UnityEngine;
using VContainer.Unity;

namespace Game
{
    public class AppController : MonoBehaviour
    {
        [Header("Scope")]
        [SerializeField] private AppScope appScope;

        private void Start()
        {
            var installers = new IInstaller[]
            {
                new Installer<MapState>(),
                new Installer<MapService>(),
                new Installer<MapVMFactory>()
            };

            appScope.Install(installers);
        }
    }
}