using System;
using UnityEngine;

namespace GameWideSystems.UIManagement
{
    public class ScreenHostsProvider : MonoBehaviour, IScreenHostProvider
    {
        [field: SerializeField] public Transform SystemHost { get; private set; }
        [field: SerializeField] public Transform GameHost { get; private set; }
        [field: SerializeField] public Transform Tooltips { get; private set; }
        [field: SerializeField] public Transform ScreenPool { get; private set; }
        [field: SerializeField] public Transform CreationHolder { get; private set; }
        [field: SerializeField] public Transform CheatConsole { get; private set; }

        public Transform GetHolderFor(ScreenHolderType screenHolderTypeType)
        {
            return screenHolderTypeType switch
            {
                ScreenHolderType.Game => GameHost,
                ScreenHolderType.System => SystemHost,
                ScreenHolderType.Tooltips => Tooltips,
                ScreenHolderType.ScreenPool => ScreenPool,
                ScreenHolderType.CheatConsole => CheatConsole,
                _ => throw new ArgumentOutOfRangeException(nameof(screenHolderTypeType), screenHolderTypeType, null)
            };
        }
    }
}