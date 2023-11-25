using Cinemachine;
using UnityEngine;

namespace _CONTENT.CodeBase.MapModule.CameraControl
{
    public class AdaptiveCamera : MonoBehaviour 
    {
        [SerializeField] private int fullWidthUnits = 180;

        void Start () 
        {
            float ratio = (float)Screen.height / (float)Screen.width;
            GetComponent<CinemachineVirtualCamera>().m_Lens.OrthographicSize = (float)fullWidthUnits * ratio / 2.0f;
        }
    }
}