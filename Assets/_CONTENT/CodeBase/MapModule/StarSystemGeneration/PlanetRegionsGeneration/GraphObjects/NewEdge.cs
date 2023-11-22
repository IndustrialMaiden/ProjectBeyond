using UnityEngine;

namespace _CONTENT.CodeBase.MapModule.StarSystemGeneration.PlanetRegionsGeneration.GraphObjects
{
    public class NewEdge
    {
        public NewCorner p0, p1;

        public NewEdge()
        {
            p0 = new NewCorner();
            p1 = new NewCorner();
        }
    }

    public class NewCorner
    {
        public Vector2 position;
    }
}