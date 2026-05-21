using System;
using System.Collections.Generic;
using UnityEngine;

namespace Structure
{
    
    /// <summary>
    /// Basically installer for any scriptable object to be bind interface to themselves
    /// </summary>
    [Tooltip("Basically installer for any scriptable object to be bind interface to themselves")]
    public class UniversalScriptableObjectClassInstaller : Zenject.ScriptableObjectInstaller
    {
        [SerializeField] private List<ScriptableObject> _dataObject = new ();

        public override void InstallBindings()
        {
            foreach (var item in _dataObject)
            {
                Container.Bind(item.GetType()).FromInstance(item).AsSingle();
            }
            
        }
    }
}