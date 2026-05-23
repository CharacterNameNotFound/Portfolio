using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;

namespace Utils.UtilityTypes.EventProcessing
{
    public class EventDispatcher<T> : IEventDispatcher<T>
    {
        private readonly List<Func<T, CancellationToken, UniTask>> _eventRegistry = new();
        
        public void RegisterHandler(Func<T, CancellationToken, UniTask> eventHandler)
        {
            _eventRegistry.Add(eventHandler);
        }

        public async UniTask Invoke(T args, CancellationToken cancellationToken)
        {
            foreach (Func<T, CancellationToken, UniTask> item in _eventRegistry)
            {
                await item.Invoke(args, cancellationToken);
            }
            
        }

        public void Unregister(Func<T, CancellationToken, UniTask> eventHandler)
        {
            _eventRegistry.Remove(eventHandler);
        }
        
        public void CleanUp()
        {
            _eventRegistry.Clear();
        }
        
    }
}