using _CONTENT.CodeBase.Infrastructure.MouseInteraction;
using UnityEngine;

namespace _CONTENT.CodeBase.MapModule.StarSystem.PlanetFarObjects
{
    public class PlanetFarSelection : MonoBehaviour, ISelectable
    {
        public void OnMouseOverSelectable()
        {
            Debug.Log($"Enter {gameObject.name}");
        }

        public void OnMouseExitSelectable()
        {
            Debug.Log($"Exit {gameObject.name}");
        }
    }
}