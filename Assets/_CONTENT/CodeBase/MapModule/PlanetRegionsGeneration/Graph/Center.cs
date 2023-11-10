using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace _CONTENT.CodeBase.MapModule.Graph
{
    public class Center
    {
        public int index;
        public Vector2 point;
        
        public List<Center> neighbors = new List<Center>();
        public List<int> neighborsIndexes = new List<int>();
        public List<Edge> borders = new List<Edge>();
        public List<Corner> corners = new List<Corner>();
        public List<NewEdge> newEdges = new List<NewEdge>();
        public Dictionary<NewEdge, List<Vector2>> NoisyEdgePoints = new Dictionary<NewEdge, List<Vector2>>();
        public List<Vector2> NoisyPoints = new List<Vector2>();

    }
}
