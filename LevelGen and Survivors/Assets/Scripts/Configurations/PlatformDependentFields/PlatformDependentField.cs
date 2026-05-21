using System;
using System.Collections.Generic;
using UnityEngine;

namespace Configurations.PlatformDependentFields
{
    [Serializable]
    public class PlatformDependentField<T>
    {
        [SerializeField] private T _invariantValue;
        [SerializeField] private bool _platformOverride;
        [SerializeField] private List<T> _platformDependantValues;

        public T Get(PlatformType platformType)
        {
            if (!_platformOverride)
            {
                return _invariantValue;
            }

            return _platformDependantValues[(int) platformType];
        }
    }
}
