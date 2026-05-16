using UnityEngine;

namespace Utils.UtilityTypes.ObjectPooling
{
    // Generated automatically on session start from binding process
    public class GenericPooledObjectHostProvider : MonoBehaviour, IPooledObjectHostProvider
    {
        private Transform _host;
        private void Awake()
        {
            _host = transform;
        }

        public Transform GetHost()
        {
            return _host;
        }
    }
}