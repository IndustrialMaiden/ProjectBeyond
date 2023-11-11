using TMPro;
using UnityEngine;

namespace _CONTENT.CodeBase.Demo
{
    public class UIDemoTooltip : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _tooltipText;

        public void SetTooltipText(string factionName)
        {
            _tooltipText.text = factionName;
        }
    }
}