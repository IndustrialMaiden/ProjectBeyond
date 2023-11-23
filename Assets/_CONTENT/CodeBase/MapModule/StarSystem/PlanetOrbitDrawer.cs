using UnityEngine;

namespace _CONTENT.CodeBase.MapModule.StarSystem
{
    [RequireComponent(typeof(LineRenderer))]
    public class PlanetOrbitDrawer : MonoBehaviour
    {
        private LineRenderer _lineRenderer;
        private float _distance;

        public void Construct(float distance)
        {
            _distance = distance;
            _lineRenderer = GetComponent<LineRenderer>();
            DrawOrbit();
        }

        private void DrawOrbit()
        {
            int segments = 100; // Количество точек для создания гладкой орбиты
            _lineRenderer.positionCount = segments;
            _lineRenderer.useWorldSpace = false;

            float horizontalAxis = _distance;
            float verticalAxis = _distance / 2;

            for (int i = 0; i < segments; i++)
            {
                float angle = ((float) i / (segments - 1)) * 2 * Mathf.PI;
                float x = Mathf.Cos(angle) * horizontalAxis;
                float y = Mathf.Sin(angle) * verticalAxis;
                _lineRenderer.SetPosition(i, new Vector3(x, y, 0));
            }
        }
    }
}