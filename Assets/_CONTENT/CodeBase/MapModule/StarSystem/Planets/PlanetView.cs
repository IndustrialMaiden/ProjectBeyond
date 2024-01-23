using System.Collections.Generic;
using _CONTENT.CodeBase.Infrastructure.MouseInteraction;
using _CONTENT.CodeBase.Infrastructure.StrategyControl;
using UnityEngine;

namespace _CONTENT.CodeBase.MapModule.StarSystem.PlanetsFar
{
    public class PlanetView : MonoBehaviour, ISelectable, IClickable
    {
        public PlanetData PlanetData { get; private set; }
        public List<PlanetView> Neighbours = new List<PlanetView>();
        
        private IStrategy _leftClickAction;

        public void Construct(PlanetData planetData)
        {
            PlanetData = planetData;
        }

        public void SetLeftClickAction(IStrategy leftClickAction)
        {
            _leftClickAction = leftClickAction;
        }

        public void AssignNeighbours(PlanetView planetView)
        {
            Neighbours.Add(planetView);
        }
        
        public void OnMouseOverSelectable()
        {
        }

        public void OnMouseExitSelectable()
        {
        }

        public void OnLeftClick()
        {
            _leftClickAction.Execute();
        }

        public void OnRightClick()
        {
        }
    }
}