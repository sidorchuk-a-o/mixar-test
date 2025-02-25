using AD.Core;
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
            };

            appScope.Install(installers);
        }
    }
}