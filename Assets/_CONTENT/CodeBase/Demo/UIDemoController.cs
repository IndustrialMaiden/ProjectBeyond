using System;
using TMPro;
using UnityEngine;

namespace _CONTENT.CodeBase.Demo
{
    public class UIDemoController : MonoBehaviour
    {
        [SerializeField] public UIDemoTooltip Tooltip;
        [SerializeField] private TextMeshProUGUI _seedText;
        [SerializeField] private TextMeshProUGUI _regionsText;


        public void SetText(int seed, int regionsCount)
        {
            _seedText.text = $"SEED: {seed}";
            _regionsText.text = $"REGIONS: {regionsCount}";
        }

        public void SetTooltip(Faction faction)
        {
            switch (faction)
            {
                case Faction.Insects:
                    Tooltip.SetTooltipText("Инсектоиды");
                    break;
                case Faction.Demons:
                    Tooltip.SetTooltipText("Демоны");
                    break;
                case Faction.Mechanoids:
                    Tooltip.SetTooltipText("Механоиды");
                    break;
                case Faction.Mages:
                    Tooltip.SetTooltipText("Волшебники");
                    break;
                case Faction.Necrons:
                    Tooltip.SetTooltipText("Некроны");
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(faction), faction, null);
            }
        }
    }
}