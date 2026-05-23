using UnityEngine;

namespace GameWideSystems.CameraManagement
{
    public class CameraManager : MonoBehaviour, ICameraManager
    {
        [field: SerializeField] public Camera MainCamera { get; private set; }
        [field: SerializeField] public Camera UICamera { get; private set; }
        
        [field: SerializeField] public GameObject MainCameraRootHolder { get; private set; }
        
        public void Initialize()
        {
        }
        
    }
}