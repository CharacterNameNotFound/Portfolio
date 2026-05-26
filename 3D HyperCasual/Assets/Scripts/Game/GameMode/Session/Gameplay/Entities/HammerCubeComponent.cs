using UnityEngine;

namespace Game.GameMode.Session.Gameplay.Entities
{
    public class HammerCubeComponent : MonoBehaviour
    {
        public Transform Transform;

        [HideInInspector] public Vector3 OriginalPosition;
        [HideInInspector] public float Cooldown;
        
    }
}