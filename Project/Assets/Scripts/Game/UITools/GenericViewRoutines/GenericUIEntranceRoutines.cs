using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

public static class GenericUIEntranceRoutines
{
    public static UniTask ShowInstantly(GameObject screenSource, CancellationToken cancellationToken)
    {
        screenSource.SetActive(true);
        
        return UniTask.CompletedTask;
    }
}
