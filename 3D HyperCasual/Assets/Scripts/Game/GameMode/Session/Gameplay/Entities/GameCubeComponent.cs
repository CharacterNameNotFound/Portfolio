using UnityEngine;
using UnityEngine.AddressableAssets;
using Utils.UtilityTypes.ObjectPooling;

namespace Game.GameMode.Session.Gameplay.Entities
{
    public class GameCubeComponent : MonoBehaviour, IPoolableEntity
    {
        private static readonly int BaseColor = Shader.PropertyToID("_BaseColor");
        
        public GameObject Cube;
        public Transform Transform;
        public Renderer Renderer;
        
        [HideInInspector] public Color Color;
        [HideInInspector] public int Line;
        [HideInInspector] public float Speed;
        
        
        public void OnPooled()
        {
            Cube.SetActive(true);
            gameObject.SetActive(false);
        }

        public void Dispose()
        {
            Addressables.ReleaseInstance(gameObject);
        }

        public void SetColor(Color color)
        {
            Color = color;
            Renderer.material.SetColor(BaseColor, color);
        }
    }
}