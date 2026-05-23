using System;
using System.Collections.Generic;
using UnityEngine;

namespace Structure.GameInstalling
{
    
    /// <summary>
    /// Basically installer for any scriptable object to be bind interface to themselves
    /// </summary>
    [Tooltip("Basically installer for any scriptable object to be bind interface to themselves")]
    public class UniversalScriptableObjectInterfaceInstaller : Zenject.ScriptableObjectInstaller
    {
        [SerializeField] private List<ScriptableObject> _dataObject = new ();

        public override void InstallBindings()
        {
            foreach (var item in _dataObject)
            {
                foreach (Type type in item.GetType().GetInterfaces())
                {
                    Container.Bind(type).FromInstance(item).AsSingle();
                }
            }
            
        }
    }
}