using TMPro;
using UnityEngine;

namespace Game.Map
{
    public class TransferCountContainer : MonoBehaviour
    {
        [SerializeField] private TMP_Text countText;

        private void Awake()
        {
            gameObject.SetActive(false);
        }

        public void Init(PathVM path)
        {
            countText.text = path.TransferCount.ToString();

            gameObject.SetActive(path.TransferCount > 0);
        }
    }
}