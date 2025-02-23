using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

namespace Game
{
    public class CubeColorChange : MonoBehaviour
    {
        [SerializeField] private XRSimpleInteractable _interactable;
        [SerializeField] private MeshRenderer _renderer;

        private void Awake()
        {
            _interactable.selectEntered.AddListener(OnSelect);
        }

        private void OnDestroy()
        {
            _interactable.selectEntered.RemoveListener(OnSelect);
        }

        private void OnSelect(SelectEnterEventArgs args)
        {
            _renderer.material.SetColor("_BaseColor", Color.HSVToRGB(Random.value, .53f, 1f));
        }
    }
}