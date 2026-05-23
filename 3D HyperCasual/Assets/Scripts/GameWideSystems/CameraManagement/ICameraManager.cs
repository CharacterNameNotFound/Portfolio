using UnityEngine;

namespace GameWideSystems.CameraManagement
{
    public interface ICameraManager
    {
        public Camera MainCamera { get; }
        public Camera UICamera { get; }
        public GameObject MainCameraRootHolder { get; }
        
        public void Initialize();
    }
}