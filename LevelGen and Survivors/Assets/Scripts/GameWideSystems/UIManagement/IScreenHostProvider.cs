using UnityEngine;

namespace GameWideSystems.UIManagement
{
    public interface IScreenHostProvider
    {
        public Transform SystemHost { get; }
        public Transform GameHost { get; }
        public Transform Tooltips { get; }
        public Transform ScreenPool { get; }
        public Transform CreationHolder { get; }
        public Transform CheatConsole { get; }

        public Transform GetHolderFor(ScreenHolderType screenHolderTypeType);

    }
}