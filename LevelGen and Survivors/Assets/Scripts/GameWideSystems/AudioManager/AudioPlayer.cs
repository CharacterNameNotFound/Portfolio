using UnityEngine;
using Utils.UtilityTypes.ObjectPooling;

namespace GameWideSystems.AudioManager
{
    public class AudioPlayer : MonoBehaviour, IPoolableEntity
    {
        [field: SerializeField] public AudioSource AudioSource { get; private set; }

        public void OnPooled() { }

        public void Dispose()
        {
            Destroy(gameObject);
        }
    }
}