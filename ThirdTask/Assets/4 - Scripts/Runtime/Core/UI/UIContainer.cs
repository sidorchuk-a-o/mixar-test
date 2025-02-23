using AD.ToolsCollection;
using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;

namespace AD.Services.Router
{
    public abstract class UIContainer : MonoBehaviour
    {
        protected readonly CompositeDisp disp = new();

        private void Awake()
        {
            disp.AddTo(this);
        }

        private void OnDestroy()
        {
            disp.Dispose();
        }
    }
}