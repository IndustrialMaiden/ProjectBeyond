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
        [SerializeField] private MeshFilter _meshFilter;
        [SerializeField] private MeshRenderer _meshRenderer;
        public int Index { get; private set; }
        public Vector2 Center { get; private set; }
        public Faction Faction { get; private set; }
        public List<Region> Neighbours { get; } = new List<Region>();
        public Vector2[] V2ColliderPositions { get; private set; }


        public void Construct(Center center, Faction faction)
        {
            Index = center.index;
            Center = center.point;
            gameObject.name = $"Region {Index}";
            Faction = faction;
            _collider.points = V2ColliderPositions = center.noisyPoints.ToArray();
            _meshFilter.mesh = MeshGenerator.CreatePolygonMesh(V2ColliderPositions);

            CreateBorder();
            ApplyFactionMaterial();
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
    }
}