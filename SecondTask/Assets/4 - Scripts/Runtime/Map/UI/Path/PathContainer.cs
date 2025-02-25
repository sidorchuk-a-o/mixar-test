using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game.Map
{
    public class PathContainer : MonoBehaviour
    {
        [Header("Segments")]
        [SerializeField] private PathSegment pathSegmentPrefab;

        [Header("Transfers")]
        [SerializeField] private TransferCountContainer transferCountContainer;

        private readonly List<PathSegment> pathSegments = new();

        public void Init(PathVM pathVM)
        {
            // init segments
            var count = Mathf.Max(pathVM.Count, pathSegments.Count);

            for (var i = 0; i < count; i++)
            {
                var pathStationVM = pathVM.Values.ElementAtOrDefault(i);
                var pathSegment = pathSegments.ElementAtOrDefault(i);

                if (pathSegment == null)
                {
                    pathSegment = Instantiate(pathSegmentPrefab, transform);
                    pathSegments.Add(pathSegment);
                }

                if (pathStationVM == null)
                {
                    pathSegment.gameObject.SetActive(false);
                    continue;
                }

                var isFirst = i == 0;
                var isLast = i == pathVM.Count - 1;

                pathSegment.Init(pathStationVM, isFirst, isLast);
                pathSegment.gameObject.SetActive(true);
            }

            transferCountContainer.Init(pathVM);
            transferCountContainer.transform.SetAsLastSibling();
        }

        public void ResetPath()
        {
            pathSegments.ForEach(x => x.gameObject.SetActive(false));
        }
    }
}