using Cinemachine;
using UnityEngine;

namespace _CONTENT.CodeBase.MapModule.CameraControl
{
    // Width Units
     
    /*public class AdaptiveCamera : MonoBehaviour 
    {
        [SerializeField] private int fullWidthUnits = 180;

        void Start() 
        {
            float ratio = (float)Screen.height / (float)Screen.width;
            GetComponent<CinemachineVirtualCamera>().m_Lens.OrthographicSize = (float)fullWidthUnits * ratio / 2.0f;
        }
    }*/
    
    // Height Units
    
    public class AdaptiveCamera : MonoBehaviour 
    {
        [SerializeField] private int fullHeightUnits = 100;

        void Awake() 
        {
            Camera mainCamera = Camera.main;
            float orthoSize = fullHeightUnits / 2.0f;
            float screenAspect = (float)Screen.width / (float)Screen.height;
            float targetAspect = mainCamera.aspect;

            if (screenAspect < targetAspect) 
            {
                orthoSize *= targetAspect / screenAspect;
            }

            GetComponent<CinemachineVirtualCamera>().m_Lens.OrthographicSize = orthoSize;
        }
    }

}