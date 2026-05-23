using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Game.UITools.GenericViewRoutines
{
    public static class GenericUIExitRoutines
    {
        public static UniTask HideInstantly(GameObject gameObject, CancellationToken cancellationToken)
        {
            gameObject.SetActive(false);
        
            return UniTask.CompletedTask;
        }
    }
}
