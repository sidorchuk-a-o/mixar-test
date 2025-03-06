using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Inputs.Readers;

namespace Game
{
    public class ScreenSpaceSelectInput : MonoBehaviour, IXRInputButtonReader
    {
        [SerializeField] private XRInputValueReader<int> screenTouchCountInput = new("Screen Touch Count");

        private bool isPerformed;
        private bool wasPerformedThisFrame;
        private bool wasCompletedThisFrame;

        protected void Update()
        {
            var prevPerformed = isPerformed;

            isPerformed = screenTouchCountInput.TryReadValue(out var count);
            wasPerformedThisFrame = !prevPerformed && isPerformed;
            wasCompletedThisFrame = prevPerformed && !isPerformed;
        }

        public bool ReadIsPerformed()
        {
            return isPerformed;
        }

        public bool ReadWasPerformedThisFrame()
        {
            return wasPerformedThisFrame;
        }

        public bool ReadWasCompletedThisFrame()
        {
            return wasCompletedThisFrame;
        }

        public float ReadValue()
        {
            return isPerformed ? 1f : 0f;
        }

        public bool TryReadValue(out float value)
        {
            value = isPerformed ? 1f : 0f;
            return isPerformed;
        }
    }
}