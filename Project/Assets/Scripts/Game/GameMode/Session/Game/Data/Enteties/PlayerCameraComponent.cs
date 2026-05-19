using Unity.Cinemachine;
using UnityEngine;

namespace Game.GameMode.Session.Game.Data.Enteties
{
    public class PlayerCameraComponent : MonoBehaviour
    {
        public Transform Transform;
        public CinemachineCamera Camera;
        [HideInInspector] public float CameraSize;
        [HideInInspector] public Bounds CameraBounds;

        public void SetSize(float size)
        {
            Camera.Lens.OrthographicSize = size;
            CameraSize = size;

            // we have camera, but I am not sure about changes to initialization pipeline in Unity 6,
            // so to be sure that we're taking data from initialized camera, getting it from Camera.main
            float aspectRatio = UnityEngine.Camera.main.aspect;

            float minY = -CameraSize;
            float maxY = CameraSize;

            float minX = -aspectRatio * size;
            float maxX = -minX;

            CameraBounds = new Bounds
            {
                min = new Vector3(minX, minY),
                max = new Vector3(maxX, maxY)
            };
        }
        
    }
}