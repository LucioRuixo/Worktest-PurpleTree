using TMPro;
using UnityEngine;

namespace Worktest_PurpleTree.UI.Gameplay
{
    public class ItemCounter : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] TMP_Text countText;

        int count = 0;

        public int Count
        {
            set
            {
                count = value;
                countText.text = "x " + value.ToString("00");
            }

            get { return count; }
        }
    }
}