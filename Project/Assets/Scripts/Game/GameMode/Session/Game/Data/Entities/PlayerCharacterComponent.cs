using UnityEngine;

namespace Game.GameMode.Session.Game.Data.Entities
{
    public class PlayerCharacterComponent : MonoBehaviour
    {
        public Transform Transform;
        public SpriteRenderer HpBar;
        public Rigidbody2D RigidBody;
        public float Radius;
    }
}