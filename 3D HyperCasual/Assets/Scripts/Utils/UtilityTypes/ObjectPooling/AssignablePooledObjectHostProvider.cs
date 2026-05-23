using UnityEngine;

namespace Utils.UtilityTypes.ObjectPooling
{
    public class AssignablePooledObjectHostProvider : IPooledObjectHostProvider
    {
        private Transform _transform;

        public AssignablePooledObjectHostProvider(Transform transform)
        {
            _transform = transform;
        }

        public Transform GetHost()
        {
            return _transform;
        }
    }
}