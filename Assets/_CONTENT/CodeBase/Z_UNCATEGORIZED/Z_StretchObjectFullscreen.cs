using UnityEngine;

namespace _CONTENT.CodeBase.Z_UNCATEGORIZED
{
    public class Z_StretchObjectFullscreen : MonoBehaviour 
    {
        void Start()
        {
            Camera mainCamera = Camera.main;
            float cameraHeight = mainCamera.orthographicSize * 2f;
            float cameraWidth = cameraHeight * mainCamera.aspect;

            float scaleX = cameraWidth;
            float scaleY = cameraHeight;

            transform.localScale = new Vector3(scaleX, scaleY, transform.localScale.z);
        }
    }
}