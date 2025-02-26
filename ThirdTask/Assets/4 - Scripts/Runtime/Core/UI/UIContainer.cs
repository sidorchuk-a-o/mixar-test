using AD.ToolsCollection;
using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;

namespace AD.Services.Router
{
    public abstract class UIContainer : MonoBehaviour
    {
        protected readonly CompositeDisp disp = new();

        protected virtual void OnEnable()
        {
            disp.AddTo(this);
        }

        protected virtual void OnDisable()
        {
            disp.Clear();
        }

        private void OnDestroy()
        {
            disp.Dispose();
        }
    }
}