using System.Collections.Generic;
using UnityEngine;

namespace _CONTENT.CodeBase.MapModule.Graph
{
    public class Corner
    {
        public int index;

        public Vector2 point;  
        public bool border;

        public List<Center> touches = new List<Center>();
        public List<Edge> protrudes = new List<Edge>();
        public List<Corner> adjacent = new List<Corner>();

    }
}
