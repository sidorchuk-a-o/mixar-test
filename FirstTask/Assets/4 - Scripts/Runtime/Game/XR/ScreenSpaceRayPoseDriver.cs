using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Inputs.Readers;

namespace Game
{
    public class ScreenSpaceRayPoseDriver : MonoBehaviour
    {
        [SerializeField] private Camera controllerCamera;
        [Space]
        [SerializeField] private XRInputValueReader<int> screenTouchCountInput = new("Screen Touch Count");
        [SerializeField] private XRInputValueReader<Vector2> touchPositionInput = new("Touch Position");
        [SerializeField] private XRInputValueReader<Vector2> tapStartPositionInput = new("Tap Start Position");
        [SerializeField] private XRInputValueReader<Vector2> dragCurrentPositionInput = new("Drag Current Position");

        private Vector2 tapStartPosition;

        protected void OnEnable()
        {
            if (controllerCamera == null)
            {
                controllerCamera = Camera.main;

                if (controllerCamera == null)
                {
                    enabled = false;
                    return;
                }
            }

            touchPositionInput.EnableDirectActionIfModeUsed();
            tapStartPositionInput.EnableDirectActionIfModeUsed();
            dragCurrentPositionInput.EnableDirectActionIfModeUsed();
            screenTouchCountInput.EnableDirectActionIfModeUsed();
        }

        protected void OnDisable()
        {
            touchPositionInput.DisableDirectActionIfModeUsed();
            tapStartPositionInput.DisableDirectActionIfModeUsed();
            dragCurrentPositionInput.DisableDirectActionIfModeUsed();
            screenTouchCountInput.DisableDirectActionIfModeUsed();
        }

        protected void Update()
        {
            var prevTapStartPosition = tapStartPosition;
            var tapPerformedThisFrame = tapStartPositionInput.TryReadValue(out tapStartPosition) && prevTapStartPosition != tapStartPosition;

            if (!screenTouchCountInput.TryReadValue(out var screenTouchCount) || screenTouchCount != 1)
            {
                return;
            }

            if (touchPositionInput.TryReadValue(out var touchPosition))
            {
                ApplyPose(touchPosition);
                return;
            }

            if (dragCurrentPositionInput.TryReadValue(out var screenPosition))
            {
                ApplyPose(screenPosition);
                return;
            }

            if (tapPerformedThisFrame)
            {
                ApplyPose(tapStartPosition);
                return;
            }
        }

        private void ApplyPose(Vector2 screenPosition)
        {
            var screenToWorldPoint = controllerCamera.ScreenToWorldPoint(new Vector3(screenPosition.x, screenPosition.y, controllerCamera.nearClipPlane));
            var directionVector = (screenToWorldPoint - controllerCamera.transform.position).normalized;

            var localPosition = transform.parent != null
                ? transform.parent.InverseTransformPoint(screenToWorldPoint)
                : screenToWorldPoint;

            transform.SetLocalPose(new Pose(localPosition, Quaternion.LookRotation(directionVector)));
        }
    }
}