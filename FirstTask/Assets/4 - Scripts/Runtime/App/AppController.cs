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
            };

            _appScope.Install(installers);
        }
    }
}