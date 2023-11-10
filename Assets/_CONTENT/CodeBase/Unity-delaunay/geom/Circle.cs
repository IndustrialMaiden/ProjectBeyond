using UnityEngine;

namespace _CONTENT.CodeBase.Unity_delaunay.geom
{	
	namespace Geo
	{
		public sealed class Circle
		{
			public Vector2 center;
			public float radius;
		
			public Circle (float centerX, float centerY, float radius)
			{
				this.center = new Vector2 (centerX, centerY);
				this.radius = radius;
			}
		
			public override string ToString ()
			{
				return "Circle (center: " + center.ToString () + "; radius: " + radius.ToString () + ")";
			}

		}
	}
}