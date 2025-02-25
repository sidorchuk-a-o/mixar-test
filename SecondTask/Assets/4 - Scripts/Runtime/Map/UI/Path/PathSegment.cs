using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Map
{
    public class PathSegment : MonoBehaviour
    {
        [SerializeField] private TMP_Text stationNameText;
        [SerializeField] private Image backgroundImage;
        [SerializeField] private Image startLineImage;
        [SerializeField] private Image endLineImage;

        public void Init(PathStationVM pathStationVM, bool isFirst, bool isLast)
        {
            stationNameText.text = pathStationVM.StationVM.Name;
            backgroundImage.color = pathStationVM.LineVM.Color;

            startLineImage.color = pathStationVM.LineVM.Color;
            endLineImage.color = pathStationVM.LineVM.Color;

            startLineImage.gameObject.SetActive(!isFirst);
            endLineImage.gameObject.SetActive(!isLast);
        }
    }
}
