using UnityEngine;

namespace Utils.UtilityTypes.ObjectPooling
{
    public interface IPooledObjectHostProvider
    {
        public Transform GetHost();
    }
}