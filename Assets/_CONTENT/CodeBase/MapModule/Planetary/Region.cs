using System;
using System.Collections.Generic;
using _CONTENT.CodeBase.MapModule.PlanetRegionsGeneration.Graph;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _CONTENT.CodeBase.MapModule.Planetary
{
    public class Region : MonoBehaviour
    {
        [SerializeField] private LineRenderer Selection;
        [SerializeField] private PolygonCollider2D Collider;
        public int Index { get; private set; }
        public Vector2 Center { get; private set; }
        public Faction Faction { get; private set; }
        public List<Region> Neighbours  { get; } = new List<Region>();
        public Vector3[] SelectionPath { get; private set; }

        public MeshRenderer MeshRenderer;

        public void Construct(Center center)
        {
            Index = center.index;
            Center = center.point;
            gameObject.name = $"Region {Index}";
            Faction = (Faction)Random.Range(0, 5);
            Collider.points = center.noisyPoints.ToArray();
            
            ApplyFactionMaterial();
            CreateBorder();
        }

        public void AddNeighbour(Region region)
        {
            Neighbours.Add(region);
        }

        public void ActivateSelection(bool state)
        {
            Selection.gameObject.SetActive(state);
        }

        private void ApplyFactionMaterial()
        {
            switch (Faction)
            {
                case Faction.Insects:
                   MeshRenderer.material = Resources.Load<Material>("Z_DemoMaterials/Insects_Mat");
                    break;
                case Faction.Demons:
                    MeshRenderer.material = Resources.Load<Material>("Z_DemoMaterials/Demons_Mat");
                    break;
                case Faction.Mechanoids:
                    MeshRenderer.material = Resources.Load<Material>("Z_DemoMaterials/Mechanoids_Mat");
                    break;
                case Faction.Mages:
                    MeshRenderer.material = Resources.Load<Material>("Z_DemoMaterials/Mages_Mat");
                    break;
                case Faction.Necrons:
                    MeshRenderer.material = Resources.Load<Material>("Z_DemoMaterials/Necrons_Mat");
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void CreateBorder()
        {
            var colliderPositions = Collider.GetPath(0);
            
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

            Selection.positionCount = SelectionPath.Length;
            Selection.SetPositions(SelectionPath);

        }
    }
}