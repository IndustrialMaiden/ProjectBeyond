using System;
using System.Collections.Generic;
using _CONTENT.CodeBase.Infrastructure.MouseInteraction;
using _CONTENT.CodeBase.MapModule.StarSystemGeneration.PlanetRegionsGeneration;
using _CONTENT.CodeBase.MapModule.StarSystemGeneration.PlanetRegionsGeneration.GraphObjects;
using UnityEngine;

namespace _CONTENT.CodeBase.MapModule.StarSystem.Regions
{
    public class Region : MonoBehaviour, ISelectable
    {
        [SerializeField] private LineRenderer _selection;
        [SerializeField] private PolygonCollider2D _collider;
        [SerializeField] private MeshFilter _meshFilter;
        [SerializeField] private MeshRenderer _meshRenderer;
        public int Index { get; private set; }
        public Vector2 Center { get; private set; }
        public Faction Faction { get; private set; }
        public List<Region> Neighbours { get; } = new List<Region>();
        public Vector2[] V2ColliderPositions { get; private set; }

        private bool _isSelectionActive;


        public void Construct(Center center, Faction faction, Material factionMaterial)
        {
            Index = center.index;
            Center = center.point;
            gameObject.name = $"Region {Index}";
            Faction = faction;
            _collider.points = V2ColliderPositions = center.noisyPoints.ToArray();
            _meshFilter.mesh = MeshGenerator.CreatePolygonMesh(V2ColliderPositions);

            CreateBorder();
            ApplyFactionMaterial(factionMaterial);
        }

        public void AddNeighbour(Region region)
        {
            Neighbours.Add(region);
        }

        private void ApplyFactionMaterial(Material factionMaterial)
        {
            _meshRenderer.material = factionMaterial;
        }

        private void CreateBorder()
        {
            Vector3[] v3ColliderPositions = new Vector3[V2ColliderPositions.Length];
            
            for (int i = 0; i < V2ColliderPositions.Length; i++)
            {
                v3ColliderPositions[i] = V2ColliderPositions[i];
            }

            var border = GetComponent<LineRenderer>();
            
            border.positionCount = v3ColliderPositions.Length;
            border.SetPositions(v3ColliderPositions);

            _selection.positionCount = v3ColliderPositions.Length;
            _selection.SetPositions(v3ColliderPositions);
        }

        public void ActivateSelection(bool state)
        {
            _selection.gameObject.SetActive(state);
        }

        public void OnMouseOverSelectable()
        {
            if (_isSelectionActive) return;
            
            ActivateSelection(true);
            _isSelectionActive = true;
        }

        public void OnMouseExitSelectable()
        {
            if (!_isSelectionActive) return;
            
            ActivateSelection(false);
            _isSelectionActive = false;
        }
    }
}