using System;
using System.Collections.Generic;
using _CONTENT.CodeBase.MapModule.StarSystemGeneration.PlanetRegionsGeneration;
using _CONTENT.CodeBase.MapModule.StarSystemGeneration.PlanetRegionsGeneration.GraphObjects;
using UnityEngine;

namespace _CONTENT.CodeBase.MapModule.StarSystem
{
    public class Region : MonoBehaviour
    {
        [SerializeField] private LineRenderer _selection;
        [SerializeField] private PolygonCollider2D _collider;
        [SerializeField] private MeshRenderer _meshRenderer;
        [SerializeField] private PolygonMesh2D _meshCreator;
        public int Index { get; private set; }
        public Vector2 Center { get; private set; }
        public Faction Faction { get; private set; }
        public List<Region> Neighbours { get; } = new List<Region>();
        public Vector3[] SelectionPath { get; private set; }


        public void Construct(Center center, Faction faction)
        {
            Index = center.index;
            Center = center.point;
            gameObject.name = $"Region {Index}";
            Faction = faction;
            _collider.points = center.noisyPoints.ToArray();
            _meshCreator.CreateMesh();
            
            ApplyFactionMaterial();
            CreateBorder();
        }

        public void AddNeighbour(Region region)
        {
            Neighbours.Add(region);
        }

        public void ActivateSelection(bool state)
        {
            _selection.gameObject.SetActive(state);
        }

        private void ApplyFactionMaterial()
        {
            switch (Faction)
            {
                case Faction.Insects:
                    _meshRenderer.material = Resources.Load<Material>("Z_DemoMaterials/Insects_Mat");
                    break;
                case Faction.Demons:
                    _meshRenderer.material = Resources.Load<Material>("Z_DemoMaterials/Demons_Mat");
                    break;
                case Faction.Mechanoids:
                    _meshRenderer.material = Resources.Load<Material>("Z_DemoMaterials/Mechanoids_Mat");
                    break;
                case Faction.Mages:
                    _meshRenderer.material = Resources.Load<Material>("Z_DemoMaterials/Mages_Mat");
                    break;
                case Faction.Necrons:
                    _meshRenderer.material = Resources.Load<Material>("Z_DemoMaterials/Necrons_Mat");
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void CreateBorder()
        {
            var colliderPositions = _collider.GetPath(0);
            
            Vector3[] linePositions = new Vector3[colliderPositions.Length];
            Vector3[] selectionPositions = new Vector3[colliderPositions.Length];
            
            for (int i = 0; i < colliderPositions.Length; i++)
            {
                linePositions[i] = new Vector3(colliderPositions[i].x, colliderPositions[i].y, -0.5f);
                selectionPositions[i] = new Vector3(colliderPositions[i].x, colliderPositions[i].y, -1f);
            }

            SelectionPath = selectionPositions;

            var border = GetComponent<LineRenderer>();
            
            border.positionCount = linePositions.Length;
            border.SetPositions(linePositions);

            _selection.positionCount = SelectionPath.Length;
            _selection.SetPositions(SelectionPath);

        }
    }
}